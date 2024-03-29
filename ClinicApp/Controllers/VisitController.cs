﻿using AutoMapper;
using ClinicApp.Infrastructure;
using ClinicApp.Infrastructure.Exceptions;
using ClinicApp.Models.Users;
using ClinicApp.Models;
using ClinicApp.Models.Dto;
using ClinicApp.Services.Schedule;
using ClinicApp.Services.User;
using ClinicApp.Services.Visit;
using ClinicApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClinicApp.Controllers
{
    public class VisitController(IUserDependenciesProvider provider, IVisitService visitService, IScheduleService scheduleService, IMapper mapper) : Controller
    {
        [Authorize(Roles = $"{Constants.Roles.PatientRoleName},{Constants.Roles.DoctorRoleName}")]
        public async Task<IActionResult> Index()
        {
            var visits = new List<Visit>();
            var id = provider.DefaultManager.GetUserId(User);

            if (User.IsInRole(Constants.Roles.PatientRoleName))
                visits.AddRange(await visitService.FindByPatientId(id ?? ""));
            else if (User.IsInRole(Constants.Roles.DoctorRoleName))
                visits.AddRange(await visitService.FindByDoctorId(id ?? ""));

            return View(new VisitListViewModel { Visits = visits });
        }

        [Authorize(Roles = Constants.Roles.PatientRoleName)]
        public async Task<IActionResult> SignUp(string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId))
                return NotFound();

            var doctor = await provider.ProvideManager<Doctor>().FindByIdAsync(doctorId);

            if (doctor == null)
                return NotFound();

            var patient = await provider.ProvideManager<Patient>().GetUserAsync(User);

            return View(new VisitViewModel
            {
                PatientId = patient?.Id,
                DoctorId = doctor.Id,
                AvailableVisits = await GetAvailableVisitDates(doctorId),
                IsActivated = patient?.IsActivated ?? false
            });
        }

        [Authorize(Roles = Constants.Roles.PatientRoleName)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(VisitViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await visitService.AddAsync(new Visit
                {
                    Date = model.Date,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId!,
                    VisitStatusId = 1
                });

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                model.AvailableVisits = await GetAvailableVisitDates(model.DoctorId);
                return View(model);
            }
        }

        [Authorize(Roles = Constants.Roles.DoctorRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            var visit = await visitService.FindByIdAsync(id);

            if (visit == null)
                return NotFound();

            return View(mapper.Map<VisitDto>(visit));
        }

        [HttpPost, Authorize(Roles = Constants.Roles.DoctorRoleName), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VisitDto visit)
        {
            if (!ModelState.IsValid)
                return View(visit);

            try
            {
                await visitService.UpdateAsync(visit.Id, mapper.Map<Visit>(visit));
                return RedirectToAction(nameof(Index));
            }
            catch (VisitConcurrencyException e)
            {
                if(e.DbEntity != null)
                    visit = mapper.Map<VisitDto>(e.DbEntity);

                ModelState.AddModelError("", e.Message);
                return View(visit);
            }
        }

        [Authorize(Roles = Constants.Roles.PatientRoleName)]
        public async Task<IActionResult> Description(int id)
        {
            var visit = await visitService.FindByIdAsync(id);

            if (visit == null)
                return NotFound();

            return View(nameof(Description), visit.Description);
        }

        [Authorize(Roles = Constants.Roles.DoctorRoleName), HttpPost]
        public async Task<IActionResult> Complete(int selectedVisitId)
        {
            try
            {
                await visitService.SetStatusAsync(selectedVisitId, VisitStatusEnum.Finished);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = $"{Constants.Roles.PatientRoleName},{Constants.Roles.DoctorRoleName}"), HttpPost]
        public async Task<IActionResult> Cancel(int selectedVisitId)
        {
            try
            {
                await visitService.SetStatusAsync(selectedVisitId, VisitStatusEnum.Cancelled);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        private async Task<IEnumerable<DateTime>> GetAvailableVisitDates(string doctorId)
        {
            var availableVisits = new List<DateTime>();
            var scheduleEntries = (await scheduleService.GetEntriesByDoctor(doctorId))
                .ToArray();

            foreach (var entry in scheduleEntries)
                availableVisits.AddRange(visitService.GetAvailableDatesForScheduleEntry(entry)
                    .Where(d => d >= DateTime.Now)
                    .OrderBy(d => d));

            return availableVisits;
        }
    }
}
