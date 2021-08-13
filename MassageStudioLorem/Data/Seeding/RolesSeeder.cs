namespace MassageStudioLorem.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Interfaces;

    using static Areas.Masseur.MasseurConstants;
    using static Areas.Admin.AdminConstants;
    using static Areas.Client.ClientConstants;

    public class RolesSeeder : ISeeder
    {
        public void Seed(LoremDbContext data, IServiceProvider serviceProvider)
        {
            var roleManager = 
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRoleName))
                        return;

                    var role = new IdentityRole { Name = AdminRoleName };

                    await roleManager.CreateAsync(role);

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
