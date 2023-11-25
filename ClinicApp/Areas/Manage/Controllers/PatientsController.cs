using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Areas.Manage.Controllers
{
    [Area(Constants.Areas.ManageAreaName), Authorize(Roles = Constants.Roles.ManagerRoleName)]
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            return View(new List<Patient>());
        }
    }
}
