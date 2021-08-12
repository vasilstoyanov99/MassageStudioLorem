namespace MassageStudioLorem.Data.Seeding
{
    using System;

    public interface ISeeder
    {
      void Seed(LoremDbContext data, IServiceProvider serviceProvider);
    }
}
