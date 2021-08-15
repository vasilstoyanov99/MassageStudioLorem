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
                var selected = appointments[0];
                appointments.Remove(selected);

                for (int i = 0; i < appointments.Count; i++)
                {
                    var review = new Review()
                    {
                        ClientFirstName = appointments[i].ClientFirstName,
                        ClientId = appointments[i].ClientId,
                        Content = ReviewSeedData.DummyReviewContent,
                        MasseurId = appointments[i].MasseurId,
                        CreatedOn = appointments[i].Date
                            .AddHours(appointments[i].Hour[1])
                    };

                    if (i % 2 == 0)
                        appointments[i].IsUserReviewedMasseur = true;

                    data.Reviews.Add(review);
                }

                data.SaveChanges();
            }
        }
    }
}
