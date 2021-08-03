namespace MassageStudioLorem.Services.Masseurs.Models
{
    public class AvailableMasseurDetailsServiceModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string MassageId { get; set; }

        public string CategoryId { get; set; }

        public double Rating { get; set; }

        public int RatersCount { get; set; }
    }
}
