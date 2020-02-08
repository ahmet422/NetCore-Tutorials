using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Models;
using NetCoreApp.ViewModels;

namespace NetCoreApp.Controllers
{
    public class HomeController : Controller
    {
         private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository) => _employeeRepository = employeeRepository;
        
        public ViewResult Index() {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        public ViewResult Details()
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee  = _employeeRepository.GetEmployee(1),
                PageTitle = "Employee Details"
            };
            

        
            return View(homeDetailsViewModel);

        }
    }
}
   