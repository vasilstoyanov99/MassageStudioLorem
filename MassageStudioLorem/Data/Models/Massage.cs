namespace MassageStudioLorem.Data.Models
{
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
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}