namespace MassageStudioLorem.Data.Seeding
{
    using Data;
    using Models;
    using System;
    using System.Linq;

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
