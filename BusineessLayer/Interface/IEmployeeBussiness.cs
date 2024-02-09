using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusineessLayer.Interface
{
    public interface IEmployeeBussiness
    {
        public IEnumerable<EmployeeEntity> GetAllEmployees();
        public EmployeeModel AddEmployee(EmployeeModel employee);
        public EmployeeEntity UpdateEmployeeDetails(EmployeeEntity employee);
        public EmployeeEntity GetEmployeeById(int employeeId);
        public EmployeeEntity DeleteFromEmployee(int EmployeeId);
        public EmployeeEntity Login(int id, string name);
        public EmployeeEntity GetEmployeeByBoth(string searchString);


    }
}
