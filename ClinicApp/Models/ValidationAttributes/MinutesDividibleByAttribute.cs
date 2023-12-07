using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models.ValidationAttributes
{
    public class MinutesDividibleByAttribute(int minutes) : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            switch (value)
            {
                case null:
                    return false;
                case TimeOnly time:
                    return time.Minute % minutes == 0;
                default:
                {
                    var dateTime = (DateTime)value;
                    return dateTime.Minute % minutes == 0;
                }
            }
        }
    }
}
