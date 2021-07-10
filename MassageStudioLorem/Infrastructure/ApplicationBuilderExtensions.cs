namespace MassageStudioLorem.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    using MassageStudioLorem.Data;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<LoremDbContext>();

            data.Database.Migrate();

            //SeedCategories(data);

            return app;
        }

        //private static void SeedCategories(CarRentingDbContext data)
        //{
        //    if (!data.Categories.Any())
        //    {
                
        //    }
        //}
    }
}
