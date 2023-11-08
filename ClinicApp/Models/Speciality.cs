using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models
{
    public class Speciality
    {
        public byte Id { get; set; }

        [StringLength(maximumLength: 256)]
        public string Name { get; set; } = string.Empty;
    }
}
