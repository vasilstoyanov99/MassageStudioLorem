namespace MassageStudioLorem.Data.Seeding
{
    using System;
    using System.Collections.Generic;

    using Interfaces;

    public class Seeder : ISeeder
    {
        public void Seed(LoremDbContext data, IServiceProvider serviceProvider)
        {
            var seeders = new List<ISeeder>() 
                { new CategoriesSeeder(), 
                    new MasseursSeeder(),
                    new MassagesSeeder(),
                    new RolesSeeder(),
                    new UsersSeeder(),
                    new AppointmentsSeeder(),
                    new ReviewsSeeder()
                };

            foreach (var seeder in seeders)
            {
                seeder.Seed(data, serviceProvider);
            }
        }
    }
}
