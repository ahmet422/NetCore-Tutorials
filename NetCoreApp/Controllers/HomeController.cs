using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Models;
using NetCoreApp.ViewModels;


namespace NetCoreApp.Controllers
{
    //[Route("[controller]/[action]")] // => we added [Route("Home")] here not to repeat it in under part
    public class HomeController : Controller
    {
         private readonly IEmployeeRepository _employeeRepository;
         private readonly IHostingEnvironment hostingEnvironment;
        // IHOstingEnvironment is used to take photo and put it under wwwroot\images\ folder
        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment) => _employeeRepository = employeeRepository;
        
        //[Route("~/Home")]
        //[Route("~/")]

        public ViewResult Index() 
        {

            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        //[Route("{id?}")]
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee  = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };
            

        
            return View(homeDetailsViewModel);

        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid) 
            {
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (IFormFile photo in model.Photos) 
                    {
                        // this line is just to return a string with a path that will be used to save the photo
                        string uploadsFolder = Path.Combine("wwwroot", "images"); // the below code is a right way but for now this works fine
                                                                                  //if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
                                                                                  //{
                                                                                  //    hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                                                                                  //}
                                                                                  //string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    
                }
                Employee newEmployee = new Employee
                { 
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                // new { id = newEmployee.Id } for automatically adding id value when new employee added
                return RedirectToAction("details", new { id = newEmployee.Id }); 
            }
            return View();
             
           
        }
    }
}
   