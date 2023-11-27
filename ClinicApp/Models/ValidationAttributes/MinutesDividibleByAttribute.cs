using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models.ValidationAttributes
{
    public class MinutesDividibleByAttribute(int minutes) : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value == null) return false;

            var time = (TimeOnly)value;
            return time.Minute % minutes == 0;
        }
    }
}
