namespace MassageStudioLorem.Areas.Admin.Models
{
    using System.Collections.Generic;

    using Services.Models;

    public class AllCategoriesViewModel
    {
        public string CategoryId { get; set; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }
    }
}
