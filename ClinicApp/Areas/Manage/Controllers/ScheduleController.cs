using ClinicApp.Areas.Manage.ViewModels;
using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Models.Users;
using ClinicApp.Services.Schedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Areas.Manage.Controllers
{
    [Area(Constants.Areas.ManageAreaName), Authorize(Roles = Constants.Roles.ManagerRoleName)]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly UserManager<Doctor> _doctorManager;

        public ScheduleController(IScheduleService scheduleService, UserManager<Doctor> doctorManager)
        {
            _scheduleService = scheduleService;
            _doctorManager = doctorManager;
        }

        public async Task<IActionResult> Index(string id, int weekNumber)
        {
            if (string.IsNullOrEmpty(id) || await _doctorManager.FindByIdAsync(id) == null)
                return NotFound();

            var entries = _scheduleService.GetEntriesByWeek(weekNumber, id);
            return View(new ScheduleEntriesViewModel { WeekNumber = weekNumber, ScheduleEntries = entries, DoctorId = id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CopyPreviousWeek(ScheduleEntriesViewModel model)
        {
            await _scheduleService.CopyPreviousWeek(model.WeekNumber - 1, model.DoctorId);
            return RedirectToAction(nameof(Index), new { id = model.DoctorId, weekNumber = model.WeekNumber });
        }

        public async Task<IActionResult> New(string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId) || await _doctorManager.FindByIdAsync(doctorId) == null)
                return NotFound();

            return View("ScheduleEntryForm", new ScheduleEntry { DoctorId = doctorId, Date = DateOnly.FromDateTime(DateTime.Now) });
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                return View("ScheduleEntryForm", await _scheduleService.GetByIdAsync(id));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEntry(ScheduleEntry entry)
        {
            if (!ModelState.IsValid) return View("ScheduleEntryForm", entry);

            try
            {
                if (entry.Id == 0)
                    await _scheduleService.AddAsync(entry);
                else await _scheduleService.UpdateAsync(entry.Id, entry);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("", e.Message);
                return View("ScheduleEntryForm", entry);
            }

            return RedirectToAction(nameof(Index), new { id = entry.DoctorId, weekNumber = entry.Date.WeekNumber() });

        }
    }
}
