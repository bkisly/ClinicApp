using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models
{
    public class Speciality
    {
        public byte Id { get; set; }

        [Required, StringLength(maximumLength: 256)]
        public string Name { get; set; } = string.Empty;
    }
}
