using ClinicApp.Areas.Manage.ViewModels;
using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.Schedule;
using ClinicApp.Services.User;
using ClinicApp.Services.Visit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Areas.Manage.Controllers
{
    [Area(Constants.Areas.ManageAreaName), Authorize(Roles = Constants.Roles.ManagerRoleName)]
    public class DoctorsController(IRegistrationService registrationService, IUserDependenciesProvider userDependenciesProvider, 
            ISpecialityRepository specialityRepository, IVisitsReportService reportService, IScheduleService scheduleService) : Controller
    {
        private DoctorRegistrationViewModel DefaultViewModel => new() { Specialities = specialityRepository.Specialities.ToList() };

        public IActionResult Index()
        {
            return View(userDependenciesProvider.ProvideManager<Doctor>().Users.Include(d => d.Speciality).ToList());
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

            var speciality = specialityRepository.GetById(viewModel.SpecialityId);
            var doctor = new Doctor
            {
                UserName = viewModel.UserName, 
                Speciality = speciality, 
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName
            };

            var result = await registrationService.RegisterAsync(doctor, viewModel.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(DefaultViewModel);

        }

        public async Task<IActionResult> Report(string doctorId, DateOnly? date = null)
        {
            date ??= DateOnly.FromDateTime(DateTime.Now);
                    
            var entries = (await scheduleService.GetEntriesByDoctor(doctorId)).Where(e => e.Date == date);
            var entriesToVisitCount = entries
                .ToDictionary(entry => entry, reportService.GetVisitsCountForScheduleEntry)
                .Where(p => p.Value != 0)
                .ToDictionary();

            return View(new VisitsReportViewModel { EntriesToVisitsCount = entriesToVisitCount, SelectedDate = date.Value, DoctorId = doctorId });
        }

        [HttpPost]
        public IActionResult FilterReport(string doctorId, DateOnly selectedDate)
        {
            return RedirectToAction(nameof(Report), new { doctorId, date = selectedDate });
        }
    }
}
