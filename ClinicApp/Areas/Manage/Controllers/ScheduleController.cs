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
    public class ScheduleController(IScheduleService scheduleService, UserManager<Doctor> doctorManager) : Controller
    {
        public async Task<IActionResult> Index(string id, int weekNumber)
        {
            if (string.IsNullOrEmpty(id) || await doctorManager.FindByIdAsync(id) == null)
                return NotFound();

            try
            {
                var entries = scheduleService.GetEntriesByWeek(weekNumber, id);
                return View(new ScheduleEntriesViewModel { WeekNumber = weekNumber, ScheduleEntries = entries, DoctorId = id });
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CopyPreviousWeek(ScheduleEntriesViewModel model)
        {
            await scheduleService.CopyPreviousWeek(model.WeekNumber - 1, model.DoctorId);
            return RedirectToAction(nameof(Index), new { id = model.DoctorId, weekNumber = model.WeekNumber });
        }

        public async Task<IActionResult> New(string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId) || await doctorManager.FindByIdAsync(doctorId) == null)
                return NotFound();

            return View("ScheduleEntryForm", new ScheduleEntry { DoctorId = doctorId, Date = DateOnly.FromDateTime(DateTime.Now) });
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                return View("ScheduleEntryForm", await scheduleService.GetByIdAsync(id));
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
                    await scheduleService.AddAsync(entry);
                else await scheduleService.UpdateAsync(entry.Id, entry);
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
