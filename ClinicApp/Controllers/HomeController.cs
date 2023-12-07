using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Services.User;
using ClinicApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers
{
    public class HomeController(IUserDependenciesProvider provider) : Controller
    {
        public IActionResult Index()
        {
            string? customContent = null;

            if (User.IsInRole(Constants.Roles.ManagerRoleName)) customContent = "Manager";
            else if (User.IsInRole(Constants.Roles.DoctorRoleName)) customContent = "Doctor";
            else if (User.IsInRole(Constants.Roles.PatientRoleName)) customContent = "Patient";

            return View(nameof(Index), customContent);
        }

        private async Task<IndexViewModel> GetViewModel()
        {
        }
    }
}
