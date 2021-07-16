namespace MassageStudioLorem.Infrastructure
{
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    using MassageStudioLorem.Data;
    using System;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<LoremDbContext>();

            data.Database.Migrate();

            SeedData(data);

            return app;
        }

        private static void SeedData(LoremDbContext data)
        {
            var masseurFromDatabase = data
                .Masseurs
                .FirstOrDefault(x => x.FirstName == "test");

            if (masseurFromDatabase == null)
            {

                var masseur = new Masseur()
                {
                    FirstName = "test",
                    LastName = "testov",
                    MiddleName = "idk",
                    DateOfBirth = DateTime.UtcNow,
                    Description = "I love to test",
                    PhoneNumber = "sheeeeshhh",
                    ProfileImagePath = "img path",
                    User = new ApplicationUser()
                    {
                        PhoneNumber = "088080480",
                        Email = "test@test.com",
                        PasswordHash = "c1227a915087c5d676f2c40ea286d8c2320d31b1fc68b6455f2550b438510cd6"
                    }
                };

                data.Masseurs.Add(masseur);
                var available = new MasseurAvailableHours()
                    { MasseurId = masseur.Id, Hour = "4:00PM", Date = "date" };
                data.MasseursAvailableHours.Add(available);

                data.SaveChanges();
            }
        }
    }
}
