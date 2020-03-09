using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Utilities;

namespace NetCoreApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action:"IsEmailInUse", controller:"Account")]
        [ValidEmailDomain(allowedDomain:"lewisu.edu", ErrorMessage = "Email domain must be lewisu.edu")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] // masking password so it cant be visible
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")] // compares password field with confirmation password fields
        public string ConfirmPassword { get; set; }

        public int Age { get; set; }
        public string City { get; set; }
    }
}
