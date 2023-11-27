using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models.ValidationAttributes
{
    public class EndLaterThanBeginAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var scheduleEntry = (ScheduleEntry)validationContext.ObjectInstance;

            if (scheduleEntry.End <= scheduleEntry.Begin)
                return new ValidationResult("End time must be earlier than begin time.",
                    new[] { nameof(scheduleEntry.End) });

            return ValidationResult.Success;
        }
    }
}
