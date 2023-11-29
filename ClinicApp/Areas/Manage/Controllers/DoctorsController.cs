using ClinicApp.Areas.Manage.ViewModels;
using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Areas.Manage.Controllers
{
    [Area(Constants.Areas.ManageAreaName), Authorize(Roles = Constants.Roles.ManagerRoleName)]
    public class DoctorsController : Controller
    {
        private readonly IRegistrationService _registrationService;
        private readonly IUserManagerProvider _userManagerProvider;
        private readonly ISpecialityRepository _specialityRepository;

        private DoctorRegistrationViewModel DefaultViewModel => new() { Specialities = _specialityRepository.Specialities.ToList() };

        public DoctorsController(IRegistrationService registrationService, IUserManagerProvider userManagerProvider, ISpecialityRepository specialityRepository)
        {
            _registrationService = registrationService;
            _userManagerProvider = userManagerProvider;
            _specialityRepository = specialityRepository;
        }

        public IActionResult Index()
        {
            return View(_userManagerProvider.Provide(new Doctor()).Users.Include(d => d.Speciality).ToList());
        }

        public IActionResult Register()
        {
            return View(DefaultViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(DoctorRegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(DefaultViewModel);

            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View(DefaultViewModel);
            }

            try
            {
                if (await _registrationService.RegisterDoctor(viewModel.UserName, viewModel.Password,
                        viewModel.SpecialityId) == RegistrationResult.Succeeded)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("UserName", "User with the specified name already exists.");
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("Password", e.Message);
            }

            return View(DefaultViewModel);

        }
    }
}
