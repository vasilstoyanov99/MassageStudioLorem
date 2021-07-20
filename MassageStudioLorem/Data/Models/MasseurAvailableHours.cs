namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MasseurAvailableHours
    {
        public MasseurAvailableHours()
        { 
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string MasseurId { get; set; }

        public Masseur Masseur { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Hour { get; set; }
    }
}
