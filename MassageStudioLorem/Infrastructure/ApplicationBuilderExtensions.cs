namespace MassageStudioLorem.Infrastructure
{
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static Areas.Masseur.MasseurConstants;
    using static Areas.Admin.AdminConstants;
    using static Areas.Client.ClientConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var services = scopedServices.ServiceProvider;
            var data = scopedServices.ServiceProvider
                .GetService<LoremDbContext>();

            data.Database.Migrate();

            SeedData(data);
            SeedRoles(services);

            return app;
        }

        private static void SeedData(LoremDbContext data)
        {
            //Masseur masseurFromDatabase = data
            //    .Masseurs
            //    .FirstOrDefault(x => x.FirstName == "test");

            //if (masseurFromDatabase == null)
            //{
            //    Masseur masseur = new Masseur()
            //    {
            //        FirstName = "test",
            //        LastName = "testov",
            //        MiddleName = "idk",
            //        Description = "I love to test",
            //        PhoneNumber = "sheeeeshhh",
            //        ProfileImagePath = "img path"
            //    };

            //    IdentityUser user = new()
            //    {
            //        PhoneNumber = "088080480",
            //        Email = "test@test.com",
            //        PasswordHash = "c1227a915087c5d676f2c40ea286d8c2320d31b1fc68b6455f2550b438510cd6"
            //    };

            //    masseur.UserId = user.Id;

            //    data.Masseurs.Add(masseur);

            //    data.SaveChanges();

            //    var available = new MasseurAvailableHours()
            //        { MasseurId = masseur.Id, Hour = "4:00PM", Date = "date" };
            //    data.MasseursAvailableHours.Add(available);
            //}

            if (data.Categories?.Count() == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Category category = new Category()
                    {
                        Name =
                            $"Test - {i}"
                    };

                    Massage massage = new Massage()
                    {
                        ImageUrl =
                            "https://www.henryford.com/-/media/henry-ford-blog/images/interior-banner-images/2018/12/massage-therapy-101.jpg?h=785&la=en&w=1920&hash=FF15876F3BCCBFBAA00F4548BA9E834C",
                        LongDescription =
                            "Swedish massage is a gentle type of full-body massage that’s ideal for people who: \r\n are new to massage, \r\n have a lot of tension, \r\n are sensitive to touch, \r\n It can help release muscle knots, \r\n and it’s also a good choice for when you want to fully relax during a massage. \r\n For this massage, you’ll remove your clothes, though you may choose to keep your underwear on. You’ll be covered with a sheet while lying on the massage table. The massage therapist will move the sheet to uncover areas that they are actively working on. \r\n The massage therapist will use a combination of: \r\n kneading \r\n long, flowing strokes in the direction of the heart \r\n deep circular motions \r\n vibration and tapping \r\n passive joint movement techniques",
                        ShortDescription =
                            "Perfect for relaxation, with pressure designed to your comfort to reduce tension.",
                        Price = 69.69,
                        Name = $"test name - {i}"
                    };

                    category.Massages.Add(massage);

                    data.Categories.Add(category);
                }

                data.SaveChanges();
            }

            //var review = new Review()
            //{
            //    ClientFirstName = "TestClient",
            //    ClientId = "7e3b0c08-14a2-4d6a-8d7c-70cb9fd14ad9",
            //    Content = "Test",
            //    CreatedOn = DateTime.Now,
            //    MasseurId = "4b3b016d-5335-41c9-80b9-0e353c64aa74"
            //};

            //data.Reviews.Add(review);
            //data.SaveChanges();

            //var appointment = new Appointment()
            //{
            //    Date = DateTime.MinValue.AddHours(1),
            //    Hour = "11:00",
            //    ClientId = "7e3b0c08-14a2-4d6a-8d7c-70cb9fd14ad9",
            //    MassageId = "0f3ff649-f3d2-4b40-891a-d73b312b7409",
            //    MasseurId = "4b3b016d-5335-41c9-80b9-0e353c64aa74",
            //    MasseurFullName = "Test Past Time",
            //    MasseurPhoneNumber = "0886650805",
            //    MassageName = "Test Past Time",
            //    IsUserReviewedMasseur = false
            //};

            //data.Appointments.Add(appointment);
            //data.SaveChanges();
        }

        private static void SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRoleName))
                        return;

                    var role = new IdentityRole { Name = AdminRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@admin.com";
                    const string adminUsername = "BestAdminEver";
                    const string adminPassword = "123456";

                    var user = new IdentityUser()
                    {
                        Email = adminEmail,
                        UserName = adminUsername
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);

                    if (await roleManager.RoleExistsAsync(MasseurRoleName))
                        return;

                    var masseurRole = new IdentityRole { Name = MasseurRoleName };

                    await roleManager.CreateAsync(masseurRole);

                    if (await roleManager.RoleExistsAsync(ClientRoleName))
                        return;

                    var clientRole = new IdentityRole { Name = ClientRoleName };

                    await roleManager.CreateAsync(clientRole);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}