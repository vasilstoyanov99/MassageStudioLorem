namespace MassageStudioLorem.Models.CustomValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ValidateDateStringAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var dateAsString = value as string;

            if (string.IsNullOrEmpty(dateAsString))
                return false;

            if (!DateTime.TryParse(dateAsString, out DateTime dateTime))
                return false;

            if (dateTime < DateTime.UtcNow.Date)
                return false;

            return true;
        }
    }
}
