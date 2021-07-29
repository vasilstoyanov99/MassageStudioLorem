namespace MassageStudioLorem.Services.Masseurs.Models
{
    public class AvailableMasseurListingServiceModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string ProfileImageUrl { get; set; }

        public double Rating { get; set; }

        public int RatersCount { get; set; }
    }
}
