namespace MassageStudioLorem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Interfaces;
    using Models;

    public class MasseursSeeder : ISeeder
    {
        public void Seed(LoremDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Masseurs.Any())
            {
                var allMasseurs = new List<Masseur>();
                var categoryIdGetter = new CategoryIdGetter();

                var energizingCategoryId = categoryIdGetter
                    .GetEnergizingCategoryId(data);
                SeedMasseursInEnergizingCategory(allMasseurs, energizingCategoryId);

                var forWomеnCategoryId = categoryIdGetter
                    .GetForWomеnCategoryId(data);
                SeedMasseursInForWomеnCategory(allMasseurs, forWomеnCategoryId);

                var relaxAndStressRelieveCategoryId = categoryIdGetter
                    .GetRelaxAndStressReliefCategoryId(data);
                SeedMasseursInRelaxAndStressReliefCategory
                    (allMasseurs, relaxAndStressRelieveCategoryId);

                var painReliefCategoryId = categoryIdGetter
                    .GetPainReliefCategoryId(data);
                SeedMasseursInPainReliefCategory(allMasseurs, painReliefCategoryId);

                data.Masseurs.AddRange(allMasseurs);
                data.SaveChanges();
            }
        }

        private static void SeedMasseursInEnergizingCategory
            (ICollection<Masseur> allMasseurs, string categoryId)
        {
            var energizingMasseur1 = new Masseur()
            {
                FullName = MasseurSeedData.EnergizingMasseur1.FullName,
                Description = MasseurSeedData.EnergizingMasseur1.Description,
                ProfileImageUrl =
                    MasseurSeedData.EnergizingMasseur1.ProfileImageUrl,
                Gender = MasseurSeedData.EnergizingMasseur1.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(energizingMasseur1);

            var energizingMasseur2 = new Masseur()
            {
                FullName = MasseurSeedData.EnergizingMasseur2.FullName,
                Description = MasseurSeedData.EnergizingMasseur2.Description,
                ProfileImageUrl =
                    MasseurSeedData.EnergizingMasseur2.ProfileImageUrl,
                Gender = MasseurSeedData.EnergizingMasseur2.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(energizingMasseur2);
        }

        private static void SeedMasseursInForWomеnCategory
            (ICollection<Masseur> allMasseurs, string categoryId)
        {
            var forWomеnMasseur1 = new Masseur()
            {
                FullName = MasseurSeedData.ForWomenMasseur1.FullName,
                Description = MasseurSeedData.ForWomenMasseur1.Description,
                ProfileImageUrl =
                    MasseurSeedData.ForWomenMasseur1.ProfileImageUrl,
                Gender = MasseurSeedData.ForWomenMasseur1.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(forWomеnMasseur1);

            var forWomеnMasseur2 = new Masseur()
            {
                FullName = MasseurSeedData.ForWomenMasseur2.FullName,
                Description = MasseurSeedData.ForWomenMasseur2.Description,
                ProfileImageUrl =
                    MasseurSeedData.ForWomenMasseur2.ProfileImageUrl,
                Gender = MasseurSeedData.ForWomenMasseur2.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(forWomеnMasseur2);

            var forWomеnMasseur3 = new Masseur()
            {
                FullName = MasseurSeedData.ForWomenMasseur3.FullName,
                Description = MasseurSeedData.ForWomenMasseur3.Description,
                ProfileImageUrl =
                    MasseurSeedData.ForWomenMasseur3.ProfileImageUrl,
                Gender = MasseurSeedData.ForWomenMasseur3.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(forWomеnMasseur3);

            var forWomеnMasseur4 = new Masseur()
            {
                FullName = MasseurSeedData.ForWomenMasseur4.FullName,
                Description = MasseurSeedData.ForWomenMasseur4.Description,
                ProfileImageUrl =
                    MasseurSeedData.ForWomenMasseur4.ProfileImageUrl,
                Gender = MasseurSeedData.ForWomenMasseur4.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(forWomеnMasseur4);
        }

        private static void SeedMasseursInRelaxAndStressReliefCategory
            (ICollection<Masseur> allMasseurs, string categoryId)
        {
            var relaxAndStressReliefMasseur1 = new Masseur()
            {
                FullName = MasseurSeedData.RelaxAndStressRelieve1.FullName,
                Description = MasseurSeedData.RelaxAndStressRelieve1.Description,
                ProfileImageUrl =
                    MasseurSeedData.RelaxAndStressRelieve1.ProfileImageUrl,
                Gender = MasseurSeedData.RelaxAndStressRelieve1.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(relaxAndStressReliefMasseur1);

            var relaxAndStressReliefMasseur2 = new Masseur()
            {
                FullName = MasseurSeedData.RelaxAndStressRelieve2.FullName,
                Description = MasseurSeedData.RelaxAndStressRelieve2.Description,
                ProfileImageUrl =
                    MasseurSeedData.RelaxAndStressRelieve2.ProfileImageUrl,
                Gender = MasseurSeedData.RelaxAndStressRelieve2.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(relaxAndStressReliefMasseur2);
        }

        private static void SeedMasseursInPainReliefCategory
            (ICollection<Masseur> allMasseurs, string categoryId)
        {
            var painReliefMasseur1 = new Masseur()
            {
                FullName = MasseurSeedData.PainRelief1.FullName,
                Description = MasseurSeedData.PainRelief1.Description,
                ProfileImageUrl =
                    MasseurSeedData.PainRelief1.ProfileImageUrl,
                Gender = MasseurSeedData.PainRelief1.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(painReliefMasseur1);

            var painReliefMasseur2 = new Masseur()
            {
                FullName = MasseurSeedData.PainRelief2.FullName,
                Description = MasseurSeedData.PainRelief2.Description,
                ProfileImageUrl =
                    MasseurSeedData.PainRelief2.ProfileImageUrl,
                Gender = MasseurSeedData.PainRelief2.Gender,
                CategoryId = categoryId
            };
            allMasseurs.Add(painReliefMasseur2);
        }
    }
}
