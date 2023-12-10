using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers
{
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        [HttpGet("{code:int}")]
        public IActionResult Index(int code) => code switch
        {
            403 => View("AccessDenied"),
            _ => NotFound()
        };
    }
}
