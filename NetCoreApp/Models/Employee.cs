using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can not exceed 50 charachters")]
        public string Name { get; set; }

        [Required] 
        [EmailAddress] 
        [Display(Name = "Office Email ")]
        public string Email { get; set; }

        [Required]
        public Dept?  Department { get; set; }
    }
}
