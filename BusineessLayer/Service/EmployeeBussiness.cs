using BusineessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusineessLayer.Service
{
    public class EmployeeBussiness : IEmployeeBussiness
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeBussiness(IEmployeeRepository employeerepository)
        {
            this.employeeRepository = employeerepository;
        }
        public IEnumerable<EmployeeEntity> GetAllEmployees()
        {
            return employeeRepository.GetAllEmployees();
        }
        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            return employeeRepository.AddEmployee(employee);
        }
        public EmployeeEntity UpdateEmployeeDetails(EmployeeEntity employee)
        {
            return employeeRepository.UpdateEmployeeDetails(employee); 
        }
        public EmployeeEntity GetEmployeeById(int employeeId)
        {
            return employeeRepository.GetEmployeeById(employeeId);
        }
        public EmployeeEntity DeleteFromEmployee(int EmployeeId)
        {
            return employeeRepository.DeleteFromEmployee(EmployeeId);
        }

        
        public EmployeeEntity Login(int id, string name)
        {
            return employeeRepository.Login(id, name);
        }
        public EmployeeEntity GetEmployeeByBoth(string searchString)
        {
            return employeeRepository.GetEmployeeByBoth(searchString);
        }


}
}
