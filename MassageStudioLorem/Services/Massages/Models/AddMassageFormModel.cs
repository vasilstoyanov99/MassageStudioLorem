namespace MassageStudioLorem.Services.Massages.Models
{
    using Masseurs.Models;
    using System.Collections.Generic;

    public class AddMassageFormModel : EditMassageFormModel
    {
        public string CategoryId { get; set; }

        public IEnumerable<MassageCategoryServiceModel> Categories
        { get; set; }
    }
}
