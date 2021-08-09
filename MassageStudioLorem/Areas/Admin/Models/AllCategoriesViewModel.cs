namespace MassageStudioLorem.Areas.Admin.Models
{
    using Services.Models;
    using System.Collections.Generic;

    public class AllCategoriesViewModel
    {
        public string CategoryId { get; set; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }
    }
}
