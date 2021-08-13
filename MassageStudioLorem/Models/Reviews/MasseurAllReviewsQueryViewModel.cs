namespace MassageStudioLorem.Models.Reviews
{
    using System.Collections.Generic;

    using Services.Reviews.Models;

    public class MasseurAllReviewsQueryViewModel
    {
        public string CategoryId { get; set; }

        public string MassageId { get; set; }

        public string MasseurId { get; set; }

        public IEnumerable<ReviewServiceModel> Reviews { get; set; }
    }
}
