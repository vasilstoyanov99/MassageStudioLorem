namespace MassageStudioLorem.Services.Massages.Models
{
    using System.Collections.Generic;

    using Masseurs.Models;

    public class AddMassageFormModel : EditMassageFormModel
    {
        public string CategoryId { get; set; }

        public IEnumerable<MassageCategoryServiceModel> Categories
        { get; set; }
    }
}
