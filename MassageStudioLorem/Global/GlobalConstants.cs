namespace MassageStudioLorem.Global
{
    public static class GlobalConstants
    {
        public static class DataValidations
        {
            public const int NameMaxLength = 26;

            public const int NameMinLength = 2;

            public const int CategoryNameMaxLength = 50;

            public const int CategoryNameMinLength = 5;

            public const int ShortDescriptionMaxLength = 150;

            public const int ShortDescriptionMinLength = 20;

            public const int LongDescriptionMaxLength = 1000;

            public const int LongDescriptionMinLength = 100;

            public const int CommentMaxLength = 500;

            public const int CommentMinLength = 50;

            public const int MassageMaxLength = 40;

            public const int MassageMinLength = 4;

            public const int MasseurDescriptionMaxLength = 150;

            public const int MasseurDescriptionMintLength = 10;

            public const string UrlRegex = @"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$";
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

        public static class Paging
        {
            public const int CurrentPageStart = 1;

            public const int CategoriesPerPage = 1;

        }
    }
}
