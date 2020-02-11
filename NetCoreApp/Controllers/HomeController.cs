﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Models;
using NetCoreApp.ViewModels;

namespace NetCoreApp.Controllers
{
    //[Route("[controller]/[action]")] // => we added [Route("Home")] here not to repeat it in under part
    public class HomeController : Controller
    {
         private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository) => _employeeRepository = employeeRepository;
        
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
    }
}
   