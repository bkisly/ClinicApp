using System.Text.Json.Serialization;
using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClinicApp.ViewModels
{
    public class VisitViewModel
    {
        public DateTime Date { get; set; }
        public string DoctorId { get; set; } = null!;
        public string? PatientId { get; set; }
        [BindNever, JsonIgnore] public bool IsActivated { get; set; }
        public IEnumerable<DateTime>? AvailableVisits { get; set; }
    }
}
 