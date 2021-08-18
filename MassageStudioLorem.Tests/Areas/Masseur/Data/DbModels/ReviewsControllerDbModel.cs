namespace MassageStudioLorem.Tests.Areas.Masseur.Data.DbModels
{
    using System;

    using MassageStudioLorem.Data.Models;
    using Services.Reviews.Models;

    public class ReviewsControllerDbModel
    {
        public static Masseur TestMasseur => new()
        {
            FullName = TestMasseurData.FullName,
            UserId = TestMasseurData.UserId,
            Id = TestMasseurData.Id
        };

        public static class TestMasseurUserData
        {
            public const string Id = "TestId";
        }

        public static Review TestReview => new()
        {
            Id = TestReviewData.Id,
            MasseurId = TestMasseur.Id,
            ClientId = TestClient.Id,
            ClientFirstName = TestClient.FirstName,
            Content = TestReviewData.Content,
            CreatedOn = DateTime.Parse("19-08-2021")
        };

        public static Client TestClient => new()
        {
            Id = TestClientData.Id,
            FirstName = TestClientData.FirstName,
        };

        public static class TestClientData
        {
            public const string FirstName = "TestClientFirstName";
            public const string Id = "TestClientId";
        }

        public static class TestMasseurData
        {
            public const string FullName = "Test Masseur";
            public const string UserId = TestMasseurUserData.Id;
            public const string Id = "TestMasseurId";
        }

        public static class TestReviewData
        {
            public const string Id = "TestReviewId";

            public const string Content = "Test Review Test Review Test Review Test Review";
        }

        public static ReviewServiceModel TestReviewModel => new()
        {
            ClientFirstName = TestClient.FirstName,
            Content = TestReview.Content,
            CreatedOn = TestReview.CreatedOn
        };
    }
}
