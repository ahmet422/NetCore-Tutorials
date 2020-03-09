using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetCoreApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        public string Gender { get; set; }

        [Range(18, 111)]
        public int Age { get; set; }
    }
}
