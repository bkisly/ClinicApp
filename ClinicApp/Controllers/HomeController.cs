using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.User;
using ClinicApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Controllers
{
    public class HomeController(IUserDependenciesProvider provider, IUserService userService, ISpecialityRepository specialityRepository) : Controller
    {
        public async Task<IActionResult> Index(int? speciality = null)
        {
            return View(nameof(Index), await GetViewModel(speciality));
        }

        private async Task<IndexViewModel> GetViewModel(int? speciality)
        {
            var viewModel = new IndexViewModel
            {
                Doctors = provider.ProvideManager<Doctor>().Users
                    .Include(d => d.Speciality)
                    .Where(d => speciality == null || d.Speciality.Id == speciality),
                RoleName = await userService.GetRoleForUser(User),
                Specialities = specialityRepository.Specialities.ToArray(),
                SelectedSpeciality = speciality
            };

            if (User.IsInRole(Constants.Roles.PatientRoleName))
            {
                var patient = await provider.ProvideManager<Patient>().GetUserAsync(User);
                viewModel.IsActivated = patient?.IsActivated ?? false;
            }

            return viewModel;
        }

        [HttpPost]
        public IActionResult Search(int? selectedSpeciality)
        {
            return RedirectToAction(nameof(Index), new { speciality = selectedSpeciality });
        }
    }
}
