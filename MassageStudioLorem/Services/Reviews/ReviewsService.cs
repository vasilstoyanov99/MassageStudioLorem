namespace MassageStudioLorem.Services.Reviews
{
    using Data;
    using Data.Models;
    using Ganss.XSS;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

            if (this.CheckIfNull(masseurFullName))
                return null;

            var clientId = this.GetClientId(userId);

            if (this.CheckIfNull(clientId))
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

            if (this.CheckIfNull(masseur))
                return false;

            var appointment = this.GetAppointmentFromDB(reviewModel.AppointmentId);

            if (this.CheckIfNull(appointment))
                return false;

            var client = this._data
                .Clients.FirstOrDefault(c => c.Id == reviewModel.ClientId);

            if (this.CheckIfNull(client))
                return false;

            return true;
        }

        public void AddNewReview(ReviewMasseurFormServiceModel reviewModel)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedContent = sanitizer.Sanitize(reviewModel.Content);
            var clientFirstName = this.GetClientFirstName(reviewModel.ClientId);
            var review = new Review()
            {
                ClientId = reviewModel.ClientId, 
                ClientFirstName = clientFirstName,
                MasseurId = reviewModel.MasseurId, 
                Content = sanitizedContent,
                CreatedOn = DateTime.Now
            };

            var appointment = this.GetAppointmentFromDB(reviewModel.AppointmentId);

            appointment.IsUserReviewedMasseur = true;

            this._data.Reviews.Add(review);
            this._data.SaveChanges();
        }

        public IEnumerable<ReviewServiceModel> GetMasseurReviews
            (string userId, string masseurId)
        {
            if (!this.CheckIfNull(userId))
                masseurId = this._data.Masseurs
                    .FirstOrDefault(m => m.UserId == userId)?.Id;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur))
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

            if (currentPage > totalReviews || currentPage < 1)
                currentPage = CurrentPageStart;

            var allReviews = reviewsAsQuery
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

            var allReviewsModel = new AllReviewsQueryServiceModel()
            {
                Reviews = allReviews,
                CurrentPage = currentPage,
                MaxPage = Math.Ceiling
                    (totalReviews * 1.00 / ReviewsPerPage * 1.00)
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

        private bool CheckIfNull(object obj)
            => obj == null;

        private string GetClientId(string userId) =>
            this._data.Clients.FirstOrDefault(c => c.UserId == userId)?.Id;

        private string GetClientFirstName(string clientId) =>
            this._data.Clients
                .FirstOrDefault(c => c.Id == clientId)?.FirstName;

        private Masseur GetMasseurFromDB(string masseurId) =>
            this._data
                .Masseurs
                .FirstOrDefault(m => m.Id == masseurId);

        private Appointment GetAppointmentFromDB(string appointmentId) =>
            this._data.Appointments.FirstOrDefault(a => a.Id == appointmentId);

        private Review GetReviewFromDB(string reviewId) =>
            this._data.Reviews.FirstOrDefault(r => r.Id == reviewId);
    }
}
