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
            throw new Exception("Error in Details View");

            Employee employee = _employeeRepository.GetEmployee(id.Value); 

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);

            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee  = employee,
                PageTitle = "Employee Details"
            };
            

        
            return View(homeDetailsViewModel);

        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            }; 
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                // change photo only if new photo is selected otherwise photo is not going to change
                if (model.Photos != null)
                {
                    // if a new photo selected we need to get rid off previoud one from the database... below code is how its done
                    if (model.ExistingPhotoPath != null)
                    {
                       string filePath = Path.Combine("wwwroot", "images", model.ExistingPhotoPath);
                       System.IO.File.Delete(filePath); 
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }

                _employeeRepository.Update(employee);
              
                return RedirectToAction("index");
            }
            return View();


        }

        private static string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null)
            {
                //foreach (IFormFile photo in model.Photos) for many selections

                // this line is just to return a string with a path that will be used to save the photo
                string uploadsFolder = Path.Combine("wwwroot", "images"); // the below code is a right way but for now this works fine
                                                                          //if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
                                                                          //{
                                                                          //    hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                                                                          //}
                                                                          //string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // the line below is needed to avoid errors related to a file being used by 2 processes at the same time (create then edit)
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photos.CopyTo(fileStream);

                }
                   


            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {  
            if (ModelState.IsValid) 
            {
                string uniqueFileName = ProcessUploadedFile(model);
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
   