namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MassageStudioLorem.Global;

    public class Category
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Massages = new HashSet<Massage>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DataValidations.NameMaxLength)]
        public string Name { get; set; }


        public ICollection<Massage> Massages { get; set; }
    }
}
