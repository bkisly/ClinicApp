using ClinicApp.Infrastructure;
using ClinicApp.Models.Dto;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string? role = null;

            if(User.IsInRole(Constants.Roles.ManagerRoleName)) role = Constants.Roles.ManagerRoleName;
            else if(User.IsInRole(Constants.Roles.DoctorRoleName)) role = Constants.Roles.DoctorRoleName;
            else if(User.IsInRole(Constants.Roles.PatientRoleName)) role = Constants.Roles.PatientRoleName;

            return View(nameof(Index), role);
        }
    }
}
