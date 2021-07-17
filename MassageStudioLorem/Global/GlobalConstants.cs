namespace MassageStudioLorem.Global
{
    public static class GlobalConstants
    {
        public static class DataValidations
        {
            public const int NameMaxLength = 50;

            public const int ShortDescriptionMaxLength = 150;

            public const int LongDescriptionMaxLength = 800;

        }

        public static class ErrorMessages
        {
            public const string Hour
                = "Please select a valid hour!";

            public const string Date
                = "Please select a valid date!";
        }

        public static class DateTimeFormats
        {
            public const string DateFormat = "dd-MM-yyyy";

            public const string TimeFormat = "h:mmtt";

            public const string DateTimeFormat = "dd-MM-yyyy h:mmtt";
        }
    }
}
