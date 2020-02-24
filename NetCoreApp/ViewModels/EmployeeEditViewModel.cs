 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApp.ViewModels
{
    public class EmployeeEditViewModel : EmployeeCreateViewModel // doing inheritance for not copying same properties again and again 
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
