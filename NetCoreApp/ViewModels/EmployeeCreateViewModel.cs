using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetCoreApp.Models;

namespace NetCoreApp.ViewModels
{
    public class EmployeeCreateViewModel
    {
    
        [Required]
        [MaxLength(50, ErrorMessage = "Name can not exceed 50 charachters")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Office Email ")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; }
        // for selecting sincle photo use "IFormFile" 
        // for selecting many use List<IFormFile>
        public IFormFile Photos { get; set; }
    }
}
