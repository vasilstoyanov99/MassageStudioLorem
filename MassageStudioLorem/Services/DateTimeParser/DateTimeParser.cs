namespace MassageStudioLorem.Services.DateTimeParser
{
    using System;
    using System.Globalization;

    using Global;

    public class DateTimeParser : IDateTimeParserService
    {
        public DateTime ConvertStrings(string date, string time)
        {
            string dateString = date + " " + time;
            string format = GlobalConstants.DateTimeFormats.DateTimeFormat;

            DateTime dateTime = DateTime.ParseExact
                (dateString, format, CultureInfo.InvariantCulture);

            return dateTime;
        }
    }
}
