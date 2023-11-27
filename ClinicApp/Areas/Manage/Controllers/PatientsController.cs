using ClinicApp.Areas.Manage.ViewModels;
using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Areas.Manage.Controllers
{
    [Area(Constants.Areas.ManageAreaName), Authorize(Roles = Constants.Roles.ManagerRoleName)]
    public class PatientsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserManagerProvider _userManagerProvider;

        private PatientsViewModel DefaultViewModel => new()
        {
            Patients = _userManagerProvider.Provide(new Patient()).Users.ToList()
        };

        public PatientsController(IUserService userService, IUserManagerProvider userManagerProvider)
        {
            _userService = userService;
            _userManagerProvider = userManagerProvider;
        }

        public IActionResult Index()
        {
            return View(DefaultViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(PatientsViewModel viewModel)
        {
            try
            {
                await _userService.ActivatePatient(viewModel.SelectedPatientId);
                return View(nameof(Index), DefaultViewModel);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
