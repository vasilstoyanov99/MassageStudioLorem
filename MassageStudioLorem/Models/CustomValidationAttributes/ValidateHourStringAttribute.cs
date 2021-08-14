namespace MassageStudioLorem.Models.CustomValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using Global;

    using static Global.DefaultHourSchedule;

    public class ValidateHourStringAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var hourAsString = value as string;

            if (string.IsNullOrEmpty(hourAsString))
                return false;

            var cultureInfo = CultureInfo.GetCultureInfo("bg-BG");

            bool isParsed = DateTime.TryParseExact(hourAsString, GlobalConstants.DateTimeFormats.HourFormat, cultureInfo, DateTimeStyles.None, out _);

            if (!isParsed)
                return false;

            if (HourScheduleAsString == null)
                SeedHourScheduleAsString();

            if (!HourScheduleAsString.Contains(hourAsString.Trim()))
                return false;

            return true;
        }
    }
}
