using ClinicApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string? customContent = null;

            if (User.IsInRole(Constants.Roles.ManagerRoleName)) customContent = "Manager";
            else if (User.IsInRole(Constants.Roles.DoctorRoleName)) customContent = "Doctor";
            else if (User.IsInRole(Constants.Roles.PatientRoleName)) customContent = "Patient";

            return View(nameof(Index), customContent);
        }
    }
}
