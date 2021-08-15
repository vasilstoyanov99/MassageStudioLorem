namespace MassageStudioLorem.Services.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ganss.XSS;

    using Data;
    using Data.Models;
    using Models;

    using static Global.GlobalConstants.Paging;

    public class ReviewsService : IReviewsService
    {
        private readonly LoremDbContext _data;

        public ReviewsService(LoremDbContext data) => this._data = data;

        public bool CheckIfClientHasLeftAReview(string appointmentId)
        {
            var appointment = this.GetAppointmentFromDB(appointmentId);
            return appointment.IsUserReviewedMasseur;
        }

        public ReviewMasseurFormServiceModel GetDataForReview
            (string userId, string masseurId, string appointmentId)
        {
            var masseurFullName = this._data.Masseurs
                .FirstOrDefault(m => m.Id == masseurId)?.FullName;

            if (CheckIfNull(masseurFullName))
                return null;

            var clientId = this.GetClientId(userId);

            if (CheckIfNull(clientId))
                return null;

            var reviewMasseurModel = new ReviewMasseurFormServiceModel()
            {
                MasseurId = masseurId, 
                ClientId = clientId, 
                MasseurFullName = masseurFullName,
                AppointmentId = appointmentId
            };

            return reviewMasseurModel;
        }

        public bool CheckIfIdsAreValid(ReviewMasseurFormServiceModel reviewModel)
        {
            var masseur = this._data.Masseurs
                .FirstOrDefault(m => m.Id == reviewModel.MasseurId);

            if (CheckIfNull(masseur))
                return false;

            var appointment = this.GetAppointmentFromDB(reviewModel.AppointmentId);

            if (CheckIfNull(appointment))
                return false;

            var client = this._data
                .Clients.FirstOrDefault(c => c.Id == reviewModel.ClientId);

            if (CheckIfNull(client))
                return false;

            return true;
        }

        public void AddNewReview(ReviewMasseurFormServiceModel reviewModel)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedContent = sanitizer.Sanitize(reviewModel.Content);
            var clientFirstName = this.GetClientFirstName(reviewModel.ClientId);
            var timeZoneOffset = this.GetClientTimeZoneOffset(reviewModel.ClientId);
            var currentDateTime = GetCurrentDateTime(timeZoneOffset);

            var review = new Review()
            {
                ClientId = reviewModel.ClientId, 
                ClientFirstName = clientFirstName,
                MasseurId = reviewModel.MasseurId, 
                Content = sanitizedContent,
                CreatedOn = currentDateTime
            };

            var appointment = this.GetAppointmentFromDB(reviewModel.AppointmentId);

            appointment.IsUserReviewedMasseur = true;

            this._data.Reviews.Add(review);
            this._data.SaveChanges();
        }

        public IEnumerable<ReviewServiceModel> GetMasseurReviews
            (string userId, string masseurId)
        {
            if (!CheckIfNull(userId))
                masseurId = this._data.Masseurs
                    .FirstOrDefault(m => m.UserId == userId)?.Id;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (CheckIfNull(masseur))
                return null;

            var reviews = this._data
                .Reviews
                .Where(r => r.MasseurId == masseurId)
                .Select(r => new ReviewServiceModel()
                {
                    ClientFirstName = r.ClientFirstName,
                    Content = r.Content,
                    CreatedOn = r.CreatedOn
                })
                .OrderBy(r => r.CreatedOn)
                .ToList();

            return reviews;
        }

        public AllReviewsQueryServiceModel GetAllReviews(int currentPage)
        {
            var reviewsAsQuery = this._data.Reviews.AsQueryable();

            if (!reviewsAsQuery.Any())
                return null;

            var totalReviews = reviewsAsQuery.Count();

            if (currentPage > GetMaxPage(totalReviews) || currentPage < 1)
                currentPage = CurrentPageStart;

            var allReviews = GetAllReviewsModels(reviewsAsQuery, currentPage);

            var allReviewsModel = new AllReviewsQueryServiceModel()
            {
                Reviews = allReviews,
                CurrentPage = currentPage,
                TotalReviews = totalReviews,
                MaxPage = GetMaxPage(totalReviews)
            };

            return allReviewsModel;
        }

        public DeleteReviewServiceModel GetReviewDataForDelete(string reviewId)
        {
            var review = this.GetReviewFromDB(reviewId);

            if (CheckIfNull(review))
                return null;

            var reviewData = new DeleteReviewServiceModel()
            {
                Id = review.Id, 
                ClientFirstName = review.ClientFirstName, 
                Content = review.Content
            };

            return reviewData;
        }

        public bool CheckIfReviewDeletedSuccessfully(string reviewId)
        {
            var review = this.GetReviewFromDB(reviewId);

            if (CheckIfNull(review))
                return false;

            this._data.Reviews.Remove(review);
            this._data.SaveChanges();

            return true;
        }

        private static bool CheckIfNull(object obj)
            => obj == null;

        private static IEnumerable<ReviewServiceModel> GetAllReviewsModels
            (IQueryable<Review> reviewsAsQuery, int currentPage)
            => reviewsAsQuery
                .Skip((currentPage - 1) * ReviewsPerPage)
                .Take(ReviewsPerPage)
                .Select(r => new ReviewServiceModel()
                {
                    Id = r.Id,
                    ClientFirstName = r.ClientFirstName,
                    Content = r.Content,
                    CreatedOn = r.CreatedOn
                })
                .OrderBy(r => r.CreatedOn)
                .ToList();

        private Masseur GetMasseurFromDB(string masseurId) =>
            this._data
                .Masseurs
                .FirstOrDefault(m => m.Id == masseurId);

        private Appointment GetAppointmentFromDB(string appointmentId) =>
            this._data.Appointments.FirstOrDefault(a => a.Id == appointmentId);

        private string GetClientId(string userId) =>
            this._data.Clients.FirstOrDefault(c => c.UserId == userId)?.Id;

        private string GetClientFirstName(string clientId) =>
            this._data.Clients
                .FirstOrDefault(c => c.Id == clientId)?.FirstName;

        private static double GetMaxPage(int count) => Math.Ceiling
            (count * 1.00 / ReviewsPerPage * 1.00);

        private double GetClientTimeZoneOffset(string clientId)
            => this._data
                .Clients
                .FirstOrDefault(c => c.Id == clientId)
                .TimeZoneOffset;

        private static DateTime GetCurrentDateTime(double timeZoneOffset)
            => DateTime.Now.AddMinutes(timeZoneOffset);

        private Review GetReviewFromDB(string reviewId) =>
            this._data.Reviews.FirstOrDefault(r => r.Id == reviewId);
    }
}
