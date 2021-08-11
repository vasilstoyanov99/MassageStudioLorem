namespace MassageStudioLorem.Models.Reviews
{
    using Services.Reviews.Models;
    using System.Collections.Generic;

    public class MasseurAllReviewsQueryViewModel
    {
        public string CategoryId { get; set; }

        public string MassageId { get; set; }

        public string MasseurId { get; set; }

        public IEnumerable<ReviewServiceModel> Reviews { get; set; }
    }
}
