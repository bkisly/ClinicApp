using ClinicApp.Areas.Manage.ViewModels;
using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Areas.Manage.Controllers
{
    [Area(Constants.Areas.ManageAreaName), Authorize(Roles = Constants.Roles.ManagerRoleName)]
    public class DoctorsController : Controller
    {
        private readonly IRegistrationService _registrationService;
        private readonly IUserManagerProvider _userManagerProvider;
        private readonly ISpecialityRepository _specialityRepository;

        public DoctorsController(IRegistrationService registrationService, IUserManagerProvider userManagerProvider, ISpecialityRepository specialityRepository)
        {
            _registrationService = registrationService;
            _userManagerProvider = userManagerProvider;
            _specialityRepository = specialityRepository;
        }

        public IActionResult Index()
        {
            return View(_userManagerProvider.Provide(new Doctor()).Users.ToList());
        }

        public IActionResult Register()
        {
            return View(new DoctorRegistrationViewModel { Specialities = _specialityRepository.Specialities.ToList() });
        }
    }
}
