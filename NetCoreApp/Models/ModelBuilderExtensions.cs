using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetCoreApp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                   new Employee
                   {
                       Id = 2,
                       Name = "Ahmet Tachmuradov",
                       Department = Dept.IT,
                       Email = "ahmet@protonmail.com"

                   },
                    new Employee
                    {
                        Id = 1,
                        Name = "Murat Tachmuradov",
                        Department = Dept.IT,
                        Email = "murat@protonmail.com"

                    },
                     new Employee
                     {
                         Id = 3,
                         Name = "Leyli Tachmuradova",
                         Department = Dept.IT,
                         Email = "leyli@protonmail.com"

                     }


                   );
        }
    }
}
