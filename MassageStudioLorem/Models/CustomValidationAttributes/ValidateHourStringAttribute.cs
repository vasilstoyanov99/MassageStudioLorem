namespace MassageStudioLorem.Models.CustomValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using Global;

    public class ValidateHourStringAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var timeString = value as string;

            if (string.IsNullOrEmpty(timeString))
            {
                return false;
            }

            bool parsed = DateTime.TryParseExact(
                timeString,
                GlobalConstants.DateTimeFormats.TimeFormat,
                CultureInfo.InvariantCulture,
                style: DateTimeStyles.AssumeUniversal,
                result: out _);

            if (!parsed)
            {
                return false;
            }

            return true;
        }
    }
}
