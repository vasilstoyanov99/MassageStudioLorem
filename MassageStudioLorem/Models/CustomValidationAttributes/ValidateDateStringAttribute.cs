namespace MassageStudioLorem.Models.CustomValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using Global;

    public class ValidateDateStringAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var dateAsString = value as string;

            if (string.IsNullOrEmpty(dateAsString))
                return false;

            bool isParsed = DateTime.TryParseExact(
                dateAsString,
                GlobalConstants.DateTimeFormats.DateTimeFormat,
                CultureInfo.InvariantCulture,
                style: DateTimeStyles.AssumeUniversal,
                result: out DateTime dateTime);

            if (!isParsed)
                return false;

            if (dateTime < DateTime.UtcNow.Date)
                return false;

            return true;
        }
    }
}
