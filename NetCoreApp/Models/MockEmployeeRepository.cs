using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApp.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id = 1, Name = "Mary", Department = "HR", Email = "sample@gmail.com"},
                new Employee() {Id = 2, Name = "Peter", Department = "IT", Email = "sample@gmail.com"},
                new Employee() {Id = 3, Name = "Andrey", Department = "Office", Email = "sample@gmail.com"}
            };

        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
