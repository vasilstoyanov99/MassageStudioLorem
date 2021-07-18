namespace MassageStudioLorem.Data.Models
{
    using Global;
    using System;
    using System.ComponentModel.DataAnnotations;

    using MassageStudioLorem.Data.Common.Models;

    public class Massage : BaseDeletableModel<string>
    {
        public Massage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(GlobalConstants.DataValidations.ShortDescriptionMaxLength)]
        public string ShortDescription { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DataValidations.LongDescriptionMaxLength)]
        public string LongDescription { get; set; }

        //[Required]
        //public string Category { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}