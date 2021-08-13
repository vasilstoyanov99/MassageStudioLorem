namespace MassageStudioLorem.Data.Seeding
{
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static Areas.Masseur.MasseurConstants;
    using static Areas.Admin.AdminConstants;
    using static Areas.Client.ClientConstants;

    public class UsersSeeder : ISeeder
    {
        public void Seed(LoremDbContext data, IServiceProvider serviceProvider)
        {
            var userManager =
                serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            Task
                .Run(async () =>
                {
                    if (!data.Users.Any
                        (u => u.UserName == UsersSeedData.Admin.Username))
                        await SeedAdmin(userManager);

                    if (!data.Users.Any
                        (u => u.UserName == UsersSeedData.Client.Username))
                        await SeedClient(data, userManager);

                    if (data.Users.Count(u => u.UserName !=
                            UsersSeedData.FemaleMasseur.Username &&
                            u.UserName !=
                            UsersSeedData.MaleMasseur.Username) == 2)
                    await SeedCommonMasseurUser(data, userManager);

                    if (!data.Users.Any
                        (u => u.UserName == UsersSeedData.MaleMasseur.Username))
                        await SeedMaleMasseur(data, userManager);

                    if (!data.Users.Any
                        (u => u.UserName == UsersSeedData.FemaleMasseur.Username))
                        await SeedFemaleMasseur(data, userManager);

                    await data.SaveChangesAsync();
                })
                .GetAwaiter()
                .GetResult();
        }

        private static async Task SeedAdmin
            (UserManager<IdentityUser> userManager)
        {
            var admin = new IdentityUser()
            {
                Email = UsersSeedData.Admin.Email,
                UserName = UsersSeedData.Admin.Username
            };

            await userManager.CreateAsync
                (admin, UsersSeedData.Admin.Password);

            await userManager.AddToRoleAsync(admin, AdminRoleName);
        }

        private static async Task SeedClient
            (LoremDbContext data, UserManager<IdentityUser> userManager)
        {
            var client = new IdentityUser()
            {
                Email = UsersSeedData.Client.Email,
                UserName = UsersSeedData.Client.Username,
                PhoneNumber = UsersSeedData.Client.PhoneNumber
            };

            await userManager.CreateAsync
                (client, UsersSeedData.Client.Password);

            await userManager.AddToRoleAsync(client, ClientRoleName);

            var newClient = new Client() 
            {
                UserId = client.Id,
                FirstName = UsersSeedData.Client.FirstName
            };
            data.Clients.Add(newClient);
        }

        private static async Task SeedCommonMasseurUser
            (LoremDbContext data, UserManager<IdentityUser> userManager)
        {
            var masseurs = data
                .Masseurs
                .Where(m => m.FullName != 
                            MasseurSeedData.EnergizingMasseur1.FullName
                            && m.FullName != MasseurSeedData.PainRelief1.FullName)
                .ToList();

            for (int i = 0; i < 8; i++)
            {
                var commonMasseur = new IdentityUser()
                {
                    Email = UsersSeedData.Common.Email + $"{i}",
                    UserName = UsersSeedData.Common.Username + $"{i}",
                    PhoneNumber = UsersSeedData.Common.PhoneNumber + $"{i}"
                };

                await userManager.CreateAsync
                    (commonMasseur, UsersSeedData.Common.Password);

                await userManager.AddToRoleAsync
                    (commonMasseur, MasseurRoleName);
                masseurs[i].UserId = commonMasseur.Id;
            }
        }

        private static async Task SeedMaleMasseur
            (LoremDbContext data, UserManager<IdentityUser> userManager)
        {
            var maleMasseur = new IdentityUser()
            {
                Email = UsersSeedData.MaleMasseur.Email,
                UserName = UsersSeedData.MaleMasseur.Username,
                PhoneNumber = UsersSeedData.MaleMasseur.PhoneNumber
            };

            await userManager.CreateAsync
                (maleMasseur, UsersSeedData.MaleMasseur.Password);

            await userManager.AddToRoleAsync(maleMasseur, MasseurRoleName);

            var masseur = data.Masseurs.FirstOrDefault
                (m => m.FullName == MasseurSeedData.PainRelief1.FullName);
            masseur.UserId = maleMasseur.Id;
        }

        private static async Task SeedFemaleMasseur
            (LoremDbContext data, UserManager<IdentityUser> userManager)
        {
            var femaleMasseur = new IdentityUser()
            {
                Email = UsersSeedData.FemaleMasseur.Email,
                UserName = UsersSeedData.FemaleMasseur.Username,
                PhoneNumber = UsersSeedData.FemaleMasseur.PhoneNumber
            };

            await userManager.CreateAsync
                (femaleMasseur, UsersSeedData.FemaleMasseur.Password);

            await userManager.AddToRoleAsync
                (femaleMasseur, MasseurRoleName);

            var masseur = data.Masseurs.FirstOrDefault
                (m => m.FullName == MasseurSeedData.EnergizingMasseur1.FullName);
            masseur.UserId = femaleMasseur.Id;
        }
    }
}
