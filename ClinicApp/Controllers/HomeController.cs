using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.Schedule;
using ClinicApp.Services.User;
using ClinicApp.Services.Visit;
using ClinicApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Controllers
{
    public class HomeController(IUserDependenciesProvider provider, IUserService userService, 
        IScheduleService scheduleService, IVisitService visitService, ISpecialityRepository specialityRepository) : Controller
    {
        public async Task<IActionResult> Index(int? speciality = null)
        {
            return View(nameof(Index), await GetViewModel(speciality));
        }

        private async Task<IndexViewModel> GetViewModel(int? speciality)
        {
            var viewModel = new IndexViewModel
            {
                Doctors = provider.ProvideManager(new Doctor()).Users
                    .Include(d => d.Speciality)
                    .Where(d => speciality == null || d.Speciality.Id == speciality),
                RoleName = await userService.GetRoleForUser(User),
                Specialities = specialityRepository.Specialities.ToArray(),
                SelectedSpeciality = speciality
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
            if (string.IsNullOrEmpty(doctorId))
                return NotFound();

            var doctor = await provider.ProvideManager(new Doctor()).FindByIdAsync(doctorId);

            if(doctor == null)
                return NotFound();

            var availableVisits = new List<DateTime>();
            var scheduleEntries = (await scheduleService.GetEntriesByDoctor(doctorId))
                .ToArray();

            foreach (var entry in scheduleEntries)
                availableVisits.AddRange(visitService.GetAvailableDatesForScheduleEntry(entry)
                    .Where(d => d >= DateTime.Now)
                    .OrderBy(d => d));

            var patient = await provider.ProvideManager(new Patient()).GetUserAsync(User);

            return View(new VisitViewModel
            {
                PatientId = patient?.Id,
                DoctorId = doctor.Id,
                AvailableVisits = availableVisits,
                IsActivated = patient?.IsActivated ?? false
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Visit(VisitViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await visitService.AddAsync(new Visit
                {
                    Date = model.Date,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId!
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Search(int? selectedSpeciality)
        {
            return RedirectToAction(nameof(Index), new { speciality = selectedSpeciality });
        }
    }
}
