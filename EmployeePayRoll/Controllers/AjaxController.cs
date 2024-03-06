using BusineessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace EmployeePayRoll.Controllers
{
    public class AjaxController : Controller
    {
        private readonly IEmployeeBussiness employeeBussiness;
        public AjaxController(IEmployeeBussiness employeeBussiness)
        {
            this.employeeBussiness = employeeBussiness;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Getallemp")]
        public IActionResult GetAllEmployeeByAjax()
        {
            List<EmployeeEntity> employee = employeeBussiness.GetAllEmployees().ToList();
            return new JsonResult(employee);
        }

    }
}
