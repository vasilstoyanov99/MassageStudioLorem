namespace MassageStudioLorem.Models.Masseurs
{
    public class MasseurDetailsViewModel
    {
        public string Id { get; set; }

        public string FirstAndLastName { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Description { get; set; }

        public string MassageId { get; set; }

        public string CategoryId { get; set; }

        public double Rating { get; set; }

        public int RatersCount { get; set; }
    }
}
