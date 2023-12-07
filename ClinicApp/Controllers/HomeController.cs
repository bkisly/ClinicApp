using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Services.User;
using ClinicApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Controllers
{
    public class HomeController(IUserDependenciesProvider provider, IUserService userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(nameof(Index), await GetViewModel());
        }

        private async Task<IndexViewModel> GetViewModel()
        {
            var viewModel = new IndexViewModel
            {
                Doctors = provider.ProvideManager(new Doctor()).Users.Include(d => d.Speciality),
                RoleName = await userService.GetRoleForUser(User)
            };

            if (User.IsInRole(Constants.Roles.PatientRoleName))
            {
                var patient = await provider.ProvideManager(new Patient()).FindByNameAsync(User.Identity!.Name!);
                viewModel.IsActivated = patient?.IsActivated ?? false;
            }

            return viewModel;
        }
    }
}
