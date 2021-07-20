namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Global;

    public class Massage
    {
        public Massage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

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