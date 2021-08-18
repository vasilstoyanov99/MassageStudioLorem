namespace MassageStudioLorem.Tests.Areas.Admin.Controllers.Data.DbModels
{
    using System;

    using MassageStudioLorem.Data.Models;

    public class ReviewsControllerTestDbModels
    {
        public static Review TestReview => new()
        {
            Id = TestReviewData.Id,
            MasseurId = TestMasseurData.Id,
            ClientId = TestClientData.Id,
            ClientFirstName = TestClientData.FirstName,
            Content = TestReviewData.Content,
            CreatedOn = DateTime.Parse("19-08-2021")
        };

        public static class TestReviewData
        {
            public const string Id = "TestReviewId";

            public const string Content = "Test Review Test Review Test Review Test Review";
        }

        public static class TestClientData
        {
            public const string FirstName = "TestClientFirstName";
            public const string Id = "TestClientId";
        }

        public static class TestMasseurData
        {
            public const string Id = "TestMasseurId";
        }
    }
}
