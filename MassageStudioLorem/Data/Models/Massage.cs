namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Global.GlobalConstants.DataValidations;

    public class Massage
    {
        public Massage()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Appointments = new List<Appointment>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(ShortDescriptionMaxLength)]
        public string ShortDescription { get; set; }

        [Required]
        [MaxLength(LongDescriptionMaxLength)]
        public string LongDescription { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        [MaxLength(MassageNameMaxLength)]
        public string Name { get; set; }

        [Required] 
        public double Price { get; set; }

        [Required]
        [RegularExpression(UrlRegex)]
        public string ImageUrl { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}