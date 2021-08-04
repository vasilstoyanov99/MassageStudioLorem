namespace MassageStudioLorem.Services.Reviews
{
    using Data;
    using Data.Models;
    using Ganss.XSS;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReviewsService : IReviewsService
    {
        private readonly LoremDbContext _data;

        public ReviewsService(LoremDbContext data) => this._data = data;

        public bool CheckIfClientHasLeftAReview(string appointmentId)
        {
            var appointment = GetAppointmentFromDB(appointmentId);
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

            var appointment = GetAppointmentFromDB(reviewModel.AppointmentId);

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

            var review = new Review()
            {
                ClientId = reviewModel.ClientId, 
                MasseurId = reviewModel.MasseurId, 
                Content = sanitizedContent
            };

            var appointment = GetAppointmentFromDB(reviewModel.AppointmentId);

            appointment.IsUserReviewedMasseur = true;

            this._data.Reviews.Add(review);
            this._data.SaveChanges();
        }

        private bool CheckIfNull(object obj)
            => obj == null;

        private string GetClientId(string userId) =>
            this._data.Clients.FirstOrDefault(c => c.UserId == userId)?.Id;

        private Appointment GetAppointmentFromDB(string appointmentId) =>
            this._data.Appointments.FirstOrDefault(a => a.Id == appointmentId);
    }
}
