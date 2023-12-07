using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Services.Schedule;
using ClinicApp.Services.User;
using ClinicApp.Services.Visit;
using ClinicApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Controllers
{
    public class HomeController(IUserDependenciesProvider provider, IUserService userService, 
        IScheduleService scheduleService, IVisitService visitService) : Controller
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

        public async Task<IActionResult> Visit(string doctorId)
        {
            var availableVisits = new List<DateTime>();
            var scheduleEntries = (await scheduleService.GetEntriesByDoctor(doctorId))
                .ToArray();

            foreach (var entry in scheduleEntries)
                availableVisits.AddRange(visitService.GetAvailableDatesForScheduleEntry(entry)
                    .Where(d => d >= DateTime.Now)
                    .OrderBy(d => d));

            return View(new VisitViewModel
            {
                DoctorId = doctorId,
                AvailableVisits = availableVisits
            });
        }
    }
}
