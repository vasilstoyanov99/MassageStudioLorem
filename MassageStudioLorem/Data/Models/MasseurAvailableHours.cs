namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class MasseurAvailableHours : BaseDeletableModel<string>
    {
        public MasseurAvailableHours()
        { 
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string MasseurId { get; set; }

        public virtual Masseur Masseur { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Hour { get; set; }
    }
}
