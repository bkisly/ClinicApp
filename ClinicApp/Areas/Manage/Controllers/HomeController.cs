using ClinicApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Areas.Manage.Controllers
{
    [Area(Constants.Areas.ManageAreaName)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Hello world! (from area index action)");
        }
    }
}
