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

            return app;
        }
    }
}