namespace MassageStudioLorem.Tests.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MassageStudioLorem.Models.Reviews;
    using Services.Appointments.Models;
    using Services.Reviews.Models;

    using static DbModels.ReviewsControllerTestDbModels;

    public class ReviewsControllerTestModels
    {
        public static PastAppointmentServiceModel PastAppointmentModel => new()
        {
            Id = TestPastAppointmentData.Id,
            Date = DateTime.Parse("16.8.2021 г. 4:53:54"),
            Hour = TestPastAppointmentData.Hour,
            ClientId = TestClient.Id,
            IsUserReviewedMasseur = false,
            MassageName = TestMassage.Name,
            MasseurId = TestMasseur.Id,
            MasseurFullName = TestMasseur.FullName,
            MasseurPhoneNumber = TestMasseurUser.PhoneNumber
        };

        public static ReviewMasseurFormServiceModel TestReviewMasseurFormModel
            => new()
            {
                MasseurId = TestMasseur.Id,
                MasseurFullName = TestMasseur.FullName,
                ClientId = TestClient.Id,
                AppointmentId = TestPastAppointment.Id,
                Content = TestPastAppointmentData.Content
            };

        public static MasseurAllReviewsQueryViewModel TestAllReviewsModel => new()
        {
            MasseurId = TestMasseur.Id,
            CategoryId = TestCategory.Id,
            MassageId = TestMassage.Id,
            Reviews = new List<ReviewServiceModel>() { GetTestReviewModel }
        };

        private static ReviewServiceModel GetTestReviewModel => new()
        {
            ClientFirstName = TestClient.FirstName,
            Content = TestReview.Content,
            CreatedOn = TestReview.CreatedOn
        };
    }
}
