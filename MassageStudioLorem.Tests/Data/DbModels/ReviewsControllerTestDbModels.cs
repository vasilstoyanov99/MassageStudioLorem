namespace MassageStudioLorem.Tests.Data.DbModels
{
    using MassageStudioLorem.Data.Enums;
    using System;

    using Microsoft.AspNetCore.Identity;

    using MassageStudioLorem.Data.Models;

    using static Models.ReviewsControllerTestModels;

    public class ReviewsControllerTestDbModels
    {
        public static Massage TestMassage => new()
        {
            Id = TestMassageData.Id,
            CategoryId = TestCategoryData.Id
        };

        public static Masseur TestMasseur => new()
        {
            CategoryId = TestCategoryData.Id,
            FullName = TestMasseurData.FullName,
            UserId = TestMasseurData.UserId,
            Id = TestMasseurData.Id
        };

        public static Category TestCategory => new()
        {
            Id = TestCategoryData.Id
        };

        public static Client TestClient => new()
        {
            Id = TestClientData.Id,
            UserId = TestUserId,
            FirstName = TestClientData.FirstName,
            TimeZoneOffset = 180
        };

        public static IdentityUser TestClientUser => new()
        {
            Id = TestUserId,
            UserName = TestClientUserData.UserName,
            PhoneNumber = TestClientUserData.PhoneNumber
        };

        public static IdentityUser TestMasseurUser => new()
        {
            Id = TestMasseurUserData.Id,
            UserName = TestMasseurUserData.UserName,
            PhoneNumber = TestMasseurUserData.PhoneNumber
        };

        public static Appointment TestPastAppointment => new()
        {
            Id = PastAppointmentModel.Id,
            Date = PastAppointmentModel.Date,
            Hour = PastAppointmentModel.Hour,
            MassageName = TestMassage.Name,
            MasseurFullName = TestMasseur.FullName,
            MasseurPhoneNumber = TestMasseurUser.PhoneNumber,
            MasseurId = TestMasseur.Id,
            MassageId = TestMassage.Id,
            ClientFirstName = TestClient.FirstName,
            ClientId = TestClient.Id,
            ClientPhoneNumber = TestClientUser.PhoneNumber,
            IsUserReviewedMasseur = false,
        };

        public static Review TestReview => new()
        {
            Id = TestReviewData.Id,
            MasseurId = TestMasseur.Id,
            ClientId = TestClient.Id,
            ClientFirstName = TestClient.FirstName,
            Content = TestReviewData.Content,
            CreatedOn = DateTime.Parse("19-08-2021")
        };

        public const string TestUserId = "TestId";

        public static class TestClientData
        {
            public const string FirstName = "TestClientFirstName";
            public const string Id = "TestClientId";
        }

        public static class TestMasseurData
        {
            public const string FullName = "Test Masseur";
            public const string UserId = "TestMasseurUserId";
            public const string Id = "TestMasseurId";
            public const Gender Gender = MassageStudioLorem.Data.Enums.Gender.Male;
        }

        public static class TestCategoryData
        {
            public const string Id = "TestCategoryId";

            public const string Name = "TestCategory";
        }

        public static class TestMassageData
        {
            public const string Id = "TestMassageId";

            public const string Name = "TestMassage";

            public const double Price = 60.00;
        }

        public static class TestUpcomingAppointmentData
        {
            public const string Id = "TestUpcomingAppointmentId";

            public const string Hour = "14:00";
        }

        public static class TestPastAppointmentData
        {
            public const string Id = "TestPastAppointmentId";

            public const string Hour = "09:00";

            public const string Content = "Test Review Test Review Test Review Test Review";
        }

        public static class TestClientUserData
        {
            public const string UserName = "TestUserUsername";

            public const string PhoneNumber = "TestClientUserPhoneNumber";
        }

        public static class TestMasseurUserData
        {
            public const string Id = "MasseurUserId";

            public const string UserName = "TestMasseurUserUsername";

            public const string PhoneNumber = "TestMasseurUserPhoneNumber";
        }

        public static class TestReviewData
        {
            public const string Id = "TestReviewId";

            public const string Content = "Test Review Test Review Test Review Test Review";
        }
    }
}
