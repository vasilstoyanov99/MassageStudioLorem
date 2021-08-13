namespace MassageStudioLorem.Data.Seeding
{
    using System;
    using System.Linq;

    using Data;
    using Interfaces;
    using Models;

    public class ReviewsSeeder : ISeeder
    {
        public void Seed(LoremDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Reviews.Any())
            {
                var appointments = data.Appointments.ToList();

                foreach (var appointment in appointments)
                {
                    var review = new Review()
                    {
                        ClientFirstName = appointment.ClientFirstName,
                        ClientId = appointment.ClientId,
                        Content = ReviewSeedData.DummyReviewContent,
                        MasseurId = appointment.MasseurId,
                        CreatedOn = appointment.Date.AddHours(
                            (int)(appointment.Hour[1]))
                    };

                    data.Reviews.Add(review);
                }

                data.SaveChanges();
            }
        }
    }
}
