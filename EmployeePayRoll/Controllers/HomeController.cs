using EmployeePayRoll.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeePayRoll.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            logger.LogWarning("Warning message");
            return View();
        } 
        

        public IActionResult Privacy()
        {
            //var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();

            //logger.LogError($"End point: {exception.Endpoint} /n Error: {exception.Error}");

            /* It will display the error message in console and debug also*/
           logger.LogError("Exception Occurs.....");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // return View();

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
