namespace MassageStudioLorem.Infrastructure
{
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Seeding;
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

            var seeder = new Seeder();
            seeder.Seed(data, services);
            SeedRoles(services);

            return app;
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