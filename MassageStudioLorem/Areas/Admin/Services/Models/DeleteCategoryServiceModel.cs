namespace MassageStudioLorem.Areas.Admin.Services.Models
{
    using System.Collections.Generic;

    public class DeleteCategoryServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Masseurs { get; set; }

        public IEnumerable<string> Massages { get; set; }
    }
}
