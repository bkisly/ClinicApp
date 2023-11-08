using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Hello world! (from controller)");
        }
    }
}
