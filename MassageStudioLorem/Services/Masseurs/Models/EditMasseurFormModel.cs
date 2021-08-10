namespace MassageStudioLorem.Services.Masseurs.Models
{
    using MassageStudioLorem.Models.Masseurs;

    public class EditMasseurFormModel : BecomeMasseurFormModel
    {
        public string Id { get; set; }

        public string CurrentCategoryName { get; set; }
    }
}
