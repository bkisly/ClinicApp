using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Services.Visit
{
    public class VisitService(IVisitRepository visitRepository) : IVisitService
    {
        public IEnumerable<DateTime> GetAvailableDatesForScheduleEntry(ScheduleEntry entry)
        {
            var allDates = new List<DateTime>();
            var takenDates = visitRepository.Visits
                .Include(v => v.Doctor)
                .Where(v => v.Doctor.Id == entry.DoctorId && v.VisitStatusId != (byte)VisitStatusEnum.Cancelled)
                .AsEnumerable()
                .Where(v => DateOnly.FromDateTime(v.Date) == entry.Date)
                .Where(v => TimeOnly.FromDateTime(v.Date) >= entry.Begin)
                .Where(v => TimeOnly.FromDateTime(v.Date) < entry.End)
                .Select(v => v.Date)
                .ToArray();

            for (var beginDateTime = new DateTime(entry.Date, entry.Begin);
                 beginDateTime < new DateTime(entry.Date, entry.End);
                 beginDateTime += TimeSpan.FromMinutes(Constants.VisitDurationMinutes))
            {
                allDates.Add(beginDateTime);
            }

            return allDates.Except(takenDates.AsEnumerable());
        }

        public async Task<IList<Models.Visit>> FindByPatientId(string patientId)
            => await visitRepository.Visits
                .Include(v => v.Patient)
                .Include(v => v.Doctor)
                .Include(v => v.VisitStatus)
                .Where(v => v.PatientId == patientId)
                .ToListAsync();

        public async Task<IList<Models.Visit>> FindByDoctorId(string doctorId)
            => await visitRepository.Visits
                .Include(v => v.Patient)
                .Include(v => v.Doctor)
                .Include(v => v.VisitStatus)
                .Where(v => v.DoctorId == doctorId)
                .ToListAsync();

        public async Task AddAsync(Models.Visit visit)
        {
            var conflictingVisits = visitRepository.Visits.Where(v => v.Date == visit.Date 
                                                                      && v.DoctorId == visit.DoctorId
                                                                      && v.VisitStatusId != (byte)VisitStatusEnum.Cancelled);

            if (conflictingVisits.Any())
                throw new ArgumentException("A visit for the selected date already exists.");

            await visitRepository.AddAsync(visit);
        }
        public async Task UpdateAsync(int visitId, Models.Visit entity) => await visitRepository.UpdateAsync(visitId, entity);
        public async Task<Models.Visit?> FindByIdAsync(int id) => await visitRepository.FindByIdAsync(id);

        public async Task SetStatusAsync(int visitId, VisitStatusEnum statusId)
        {
            var visit = await visitRepository.FindByIdAsync(visitId) 
                        ?? throw new InvalidOperationException($"Visit with ID {visitId} not found.");

            visit.VisitStatusId = (byte)statusId;
            await visitRepository.UpdateAsync(visitId, visit);
        }
    }
}
