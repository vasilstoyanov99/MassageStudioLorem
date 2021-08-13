namespace MassageStudioLorem.Data.Seeding
{
    using Data;
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MassagesSeeder : ISeeder
    {
        public void Seed(LoremDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Massages.Any())
            {
                var allMassages = new List<Massage>();
                var categoryIdGetter = new CategoryIdGetter();

                var energizingCategoryId = categoryIdGetter
                    .GetEnergizingCategoryId(data);
                SeedMassagesInEnergizingCategory
                    (allMassages, energizingCategoryId);

                var forWomеnCategoryId = categoryIdGetter
                    .GetForWomеnCategoryId(data);
                SeedMassagesInForWomеnCategory(allMassages, forWomеnCategoryId);

                var relaxAndStressRelieveCategoryId = categoryIdGetter
                        .GetRelaxAndStressReliefCategoryId(data);
                SeedMassagesInRelaxAndStressReliefCategory
                    (allMassages, relaxAndStressRelieveCategoryId);

                var painReliefCategoryId = categoryIdGetter
                    .GetPainReliefCategoryId(data);
                SeedMassagesInPainReliefCategory(allMassages, painReliefCategoryId);

                data.Massages.AddRange(allMassages);
                data.SaveChanges();
            }
        }

        private static void SeedMassagesInEnergizingCategory
            (ICollection<Massage> allMassages, string categoryId)
        {
            var thai = new Massage()
            {
                Name = MassagesSeedData.Thai.Name,
                ShortDescription = MassagesSeedData.Thai.ShortDescription,
                LongDescription = MassagesSeedData.Thai.LongDescription,
                ImageUrl = MassagesSeedData.Thai.ImageUrl,
                Price = MassagesSeedData.Thai.Price,
                CategoryId = categoryId
            };
            allMassages.Add(thai);

            var sportsPreWorkout = new Massage()
            {
                Name = MassagesSeedData.SportsPreWorkout.Name,
                ShortDescription =
                    MassagesSeedData.SportsPreWorkout.ShortDescription,
                LongDescription =
                    MassagesSeedData.SportsPreWorkout.LongDescription,
                ImageUrl = MassagesSeedData.SportsPreWorkout.ImageUrl,
                Price = MassagesSeedData.SportsPreWorkout.Price,
                CategoryId = categoryId
            };
            allMassages.Add(sportsPreWorkout);
        }

        private static void SeedMassagesInForWomеnCategory
            (ICollection<Massage> allMassages, string categoryId)
        {
            var beauty = new Massage()
            {
                Name = MassagesSeedData.Beauty.Name,
                ShortDescription = MassagesSeedData.Beauty.ShortDescription,
                LongDescription = MassagesSeedData.Beauty.LongDescription,
                ImageUrl = MassagesSeedData.Beauty.ImageUrl,
                Price = MassagesSeedData.Beauty.Price,
                CategoryId = categoryId
            };
            allMassages.Add(beauty);

            var antiCellulite = new Massage()
            {
                Name = MassagesSeedData.AntiCellulite.Name,
                ShortDescription = MassagesSeedData.AntiCellulite.ShortDescription,
                LongDescription = MassagesSeedData.AntiCellulite.LongDescription,
                ImageUrl = MassagesSeedData.AntiCellulite.ImageUrl,
                Price = MassagesSeedData.AntiCellulite.Price,
                CategoryId = categoryId
            };
            allMassages.Add(antiCellulite);

            var prenatal = new Massage()
            {
                Name = MassagesSeedData.Prenatal.Name,
                ShortDescription = MassagesSeedData.Prenatal.ShortDescription,
                LongDescription = MassagesSeedData.Prenatal.LongDescription,
                ImageUrl = MassagesSeedData.Prenatal.ImageUrl,
                Price = MassagesSeedData.Prenatal.Price,
                CategoryId = categoryId
            };
            allMassages.Add(prenatal);
        }

        private static void SeedMassagesInRelaxAndStressReliefCategory
            (ICollection<Massage> allMassages, string categoryId)
        {
            var shiatsu = new Massage()
            {
                Name = MassagesSeedData.Shiatsu.Name,
                ShortDescription = MassagesSeedData.Shiatsu.ShortDescription,
                LongDescription = MassagesSeedData.Shiatsu.LongDescription,
                ImageUrl = MassagesSeedData.Shiatsu.ImageUrl,
                Price = MassagesSeedData.Shiatsu.Price,
                CategoryId = categoryId
            };
            allMassages.Add(shiatsu);

            var hotStone = new Massage()
            {
                Name = MassagesSeedData.HotStone.Name,
                ShortDescription = MassagesSeedData.HotStone.ShortDescription,
                LongDescription = MassagesSeedData.HotStone.LongDescription,
                ImageUrl = MassagesSeedData.HotStone.ImageUrl,
                Price = MassagesSeedData.HotStone.Price,
                CategoryId = categoryId
            };
            allMassages.Add(hotStone);

            var reflexology = new Massage()
            {
                Name = MassagesSeedData.Reflexology.Name,
                ShortDescription = MassagesSeedData.Reflexology.ShortDescription,
                LongDescription = MassagesSeedData.Reflexology.LongDescription,
                ImageUrl = MassagesSeedData.Reflexology.ImageUrl,
                Price = MassagesSeedData.Reflexology.Price,
                CategoryId = categoryId
            };
            allMassages.Add(reflexology);

            var aromatherapy = new Massage()
            {
                Name = MassagesSeedData.Aromatherapy.Name,
                ShortDescription = MassagesSeedData.Aromatherapy.ShortDescription,
                LongDescription = MassagesSeedData.Aromatherapy.LongDescription,
                ImageUrl = MassagesSeedData.Aromatherapy.ImageUrl,
                Price = MassagesSeedData.Aromatherapy.Price,
                CategoryId = categoryId
            };
            allMassages.Add(aromatherapy);

            var swedish = new Massage()
            {
                Name = MassagesSeedData.Swedish.Name,
                ShortDescription = MassagesSeedData.Swedish.ShortDescription,
                LongDescription = MassagesSeedData.Swedish.LongDescription,
                ImageUrl = MassagesSeedData.Swedish.ImageUrl,
                Price = MassagesSeedData.Swedish.Price,
                CategoryId = categoryId
            };
            allMassages.Add(swedish);
        }

        private static void SeedMassagesInPainReliefCategory
            (ICollection<Massage> allMassages, string categoryId)
        {
            var triggerPoint = new Massage()
            {
                Name = MassagesSeedData.TriggerPoint.Name,
                ShortDescription = MassagesSeedData.TriggerPoint.ShortDescription,
                LongDescription = MassagesSeedData.TriggerPoint.LongDescription,
                ImageUrl = MassagesSeedData.TriggerPoint.ImageUrl,
                Price = MassagesSeedData.TriggerPoint.Price,
                CategoryId = categoryId
            };
            allMassages.Add(triggerPoint);

            var sportsAfterWorkout = new Massage()
            {
                Name = MassagesSeedData.SportsAfterWorkout.Name,
                ShortDescription = 
                    MassagesSeedData.SportsAfterWorkout.ShortDescription,
                LongDescription = 
                    MassagesSeedData.SportsAfterWorkout.LongDescription,
                ImageUrl = MassagesSeedData.SportsAfterWorkout.ImageUrl,
                Price = MassagesSeedData.SportsAfterWorkout.Price,
                CategoryId = categoryId
            };
            allMassages.Add(sportsAfterWorkout);

            var deepTissue = new Massage()
            {
                Name = MassagesSeedData.DeepTissue.Name,
                ShortDescription =
                    MassagesSeedData.DeepTissue.ShortDescription,
                LongDescription =
                    MassagesSeedData.DeepTissue.LongDescription,
                ImageUrl = MassagesSeedData.DeepTissue.ImageUrl,
                Price = MassagesSeedData.DeepTissue.Price,
                CategoryId = categoryId
            };
            allMassages.Add(deepTissue);
        }
    }
}
