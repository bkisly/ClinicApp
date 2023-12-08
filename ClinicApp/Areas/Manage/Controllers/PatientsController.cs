using ClinicApp.Areas.Manage.ViewModels;
using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Areas.Manage.Controllers
{
    [Area(Constants.Areas.ManageAreaName), Authorize(Roles = Constants.Roles.ManagerRoleName)]
    public class PatientsController(IUserService userService, IUserDependenciesProvider userDependenciesProvider)
        : Controller
    {
        private PatientsViewModel DefaultViewModel => new()
        {
            Patients = userDependenciesProvider.ProvideManager<Patient>().Users.ToList()
        };

        public IActionResult Index()
        {
            return View(DefaultViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(PatientsViewModel viewModel)
        {
            try
            {
                await userService.ActivatePatient(viewModel.SelectedPatientId);
                return View(nameof(Index), DefaultViewModel);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
