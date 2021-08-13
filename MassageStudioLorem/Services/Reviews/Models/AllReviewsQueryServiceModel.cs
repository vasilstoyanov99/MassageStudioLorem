namespace MassageStudioLorem.Services.Reviews.Models
{
    using System.Collections.Generic;

    using static Global.GlobalConstants.Paging;

    public class AllReviewsQueryServiceModel
    {
        public AllReviewsQueryServiceModel() => this.CurrentPage = CurrentPageStart;

        public int CurrentPage { get; set; }

        public double MaxPage { get; set; }

        public IEnumerable<ReviewServiceModel> Reviews { get; set; }

    }
}
