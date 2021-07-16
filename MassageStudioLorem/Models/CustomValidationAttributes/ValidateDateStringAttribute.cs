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
            var dateString = value as string;

            if (string.IsNullOrEmpty(dateString))
            {
                return false;
            }

            bool parsed = DateTime.TryParseExact(
                            dateString,
                            GlobalConstants.DateTimeFormats.DateFormat,
                            CultureInfo.InvariantCulture,
                            style: DateTimeStyles.AssumeUniversal,
                            result: out DateTime dateTime);
            if (!parsed)
            {
                return false;
            }

            if (dateTime < DateTime.UtcNow.Date)
            {
                return false;
            }

            return true;
        }
    }
}
