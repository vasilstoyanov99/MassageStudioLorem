namespace MassageStudioLorem.Data.Seeding.Interfaces
{
    using System;

    public interface ISeeder
    {
      void Seed(LoremDbContext data, IServiceProvider serviceProvider);
    }
}
