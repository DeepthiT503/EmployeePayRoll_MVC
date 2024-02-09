using BusineessLayer.Interface;
using BusineessLayer.Service;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace EmployeePayRoll.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBussiness employeeBussiness;
       private readonly ILogger<EmployeeController> logger;
        // private readonly ILogger logger;
        public EmployeeController(IEmployeeBussiness employeeBussiness, ILogger<EmployeeController> logger)
        {
            this.employeeBussiness = employeeBussiness;
            this.logger = logger;
        }
        //If u dont want to specity the name of the application then you can use ILoggerFactory
        //public EmployeeController(IEmployeeBussiness employeeBussiness, ILoggerFactory factory)
        //{
        //    this.employeeBussiness = employeeBussiness;
        //    this.logger = factory.CreateLogger("custom category");
        //}

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("getAll")]
        public IActionResult GetllEmployye() 
        {
            logger.LogInformation(101, "Received Information from GetAllEmployee method");
            List<EmployeeEntity> lstEmployee = employeeBussiness.GetAllEmployees().ToList();
            return View(lstEmployee);

        }
        // [HttpGet("getByBoth/{name}")]
        //public IActionResult GetAllEmpByNameOrDepartment(string name)
        //{

        //    try
        //    {

        //        List<EmployeeEntity> employee = employeeBussiness.GetAllEmployeesByName(name).ToList();

        //        if (employee == null) 
        //        {
        //            return NotFound();

        //        }

        //        return View(employee);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
        //        return View();
        //    }
        //}
        [HttpGet("getByName/{both}")]
        public IActionResult GetEmployeeByBoth(string searchString)
        {
                var employee = employeeBussiness.GetEmployeeByBoth(searchString);

            return View(employee); 
        }


        [HttpGet("insertEmp")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("insertEmp")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeBussiness.AddEmployee(model);
                    return RedirectToAction("GetllEmployye");
                }
                return View(model);
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(model);
            }
        }
        //for getting edit from

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeEntity employee = employeeBussiness.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        //for updating the Edit form
        [HttpPost]
        [Route("Edit/{id}"),ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] EmployeeEntity employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                {
                    return NotFound(); // Use NotFound() instead of NotFoundResult()
                }

                if (ModelState.IsValid)
                {
                    employeeBussiness.UpdateEmployeeDetails(employee);
                    return RedirectToAction("GetllEmployye");
                }

                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(employee);
            }
        }
        [HttpGet]
        [Route("getById")]
        public IActionResult Details(int id)
        {

            try
            {
                EmployeeEntity employee = employeeBussiness.GetEmployeeById(id);

                if (employee == null)
                {
                    return NotFound();

                }

                return View(employee);
            }
            catch (Exception ex)
            {

                return View();
            }
        }
            [HttpGet]
            public IActionResult Delete(int id)
            {
                try
                {
                    if (id == null)
                    {
                        return NotFound();
                    }
                    EmployeeEntity employee = employeeBussiness.GetEmployeeById(id);
                    if (employee == null)
                    {
                        return NotFound();
                    }
                    return View(employee);
                }
                catch (Exception ex)
                {

                    return RedirectToAction("GetllEmployye");
                }

            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
            {
            try
            {
                employeeBussiness.DeleteFromEmployee(id);
                return RedirectToAction("GetllEmployye");
            }
            catch (Exception ex)
            {

                return RedirectToAction("GetllEmployye");
            }
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login([Bind] LoginModel model)
        {
            try
            {

                if (model.EmployeeId <= 0 || string.IsNullOrEmpty(model.FullName))
                {

                    return BadRequest($"Invalid input parameters {model.EmployeeId} or {model.FullName}");
                }


                EmployeeEntity employee = employeeBussiness.Login(model.EmployeeId, model.FullName);

                if (employee == null)
                {

                    return BadRequest($"Invalid input parameters {model.EmployeeId} or {model.FullName} Please enter valid Input");
                }

                HttpContext.Session.SetInt32("EmployeeId", employee.EmployeeId);
                HttpContext.Session.SetString("FullName", employee.FullName);

                return RedirectToAction("Details", new { id = model.EmployeeId });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}

