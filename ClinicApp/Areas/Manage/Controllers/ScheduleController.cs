using ClinicApp.Areas.Manage.ViewModels;
using ClinicApp.Infrastructure;
using ClinicApp.Services.Schedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Areas.Manage.Controllers
{
    [Area(Constants.Areas.ManageAreaName), Authorize(Roles = Constants.Roles.ManagerRoleName)]
    public class ScheduleController(IScheduleService scheduleService) : Controller
    {
        public IActionResult Index(string id, int weekNumber)
        {
            var entries = scheduleService.GetEntriesByWeek(DateOnly.FromDateTime(DateTime.Now).WeekNumber(), id);
            return View(new ScheduleEntriesViewModel { WeekNumber = weekNumber, ScheduleEntries = entries, DoctorId = id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CopyPreviousWeek(ScheduleEntriesViewModel model)
        {
            await scheduleService.CopyPreviousWeek(model.WeekNumber - 1, model.DoctorId);
            return RedirectToAction(nameof(Index), new { id = model.DoctorId, weekNumber = model.WeekNumber });
        }
    }
}
