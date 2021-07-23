namespace MassageStudioLorem.Global
{
    public static class GlobalConstants
    {
        public static class DataValidations
        {
            //public const int NameMaxLength = 26;

            //public const int NameMinLength = 2;

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

            public const int MasseurDescriptionMaxLength = 1000;

            public const int MasseurDescriptionMinLength = 20;

            public const string UrlRegex = @"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$";
        }

        public static class ErrorMessages
        {
            public const string Hour
                = "Please select a valid hour!";

            public const string Date
                = "Please select a valid date!";

            public const string InvalidUrl
                = "The provided url is invalid!";
                    
            //TODO: Find a way to make this not hard-coded!

            public const string MasseurDescription
                = "The provided description must be between {1} and {2} characters long!";

            public const string PasswordLength
                = "The {0} must be at least {2} and at max {1} characters long.";

            public const string PasswordConformation
                = "The password and confirmation password do not match.";

            public const string CategoryIdError = "Category does not exist.";

            public const string GenderIdError = "Please choose between the provided options!";

            public const string AlreadyMasseur = "You are already a masseur!";
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
