namespace MassageStudioLorem.Models.CustomValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Global.DefaultHourSchedule;

    public class ValidateHourStringAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var hourAsString = value as string;

            if (string.IsNullOrEmpty(hourAsString))
                return false;

            if (!DateTime.TryParse(hourAsString, out DateTime _))
                return false;

            if (HourScheduleAsString == null)
                SeedHourScheduleAsString();

            if (!HourScheduleAsString.Contains(hourAsString.Trim()))
                return false;

            return true;
        }
    }
}
