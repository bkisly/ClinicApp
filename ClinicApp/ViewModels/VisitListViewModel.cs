using ClinicApp.Models;

namespace ClinicApp.ViewModels
{
    public class VisitListViewModel
    {
        public IEnumerable<Visit> Visits { get; set; } = null!;
        public int SelectedVisitId { get; set; }
    }
}
