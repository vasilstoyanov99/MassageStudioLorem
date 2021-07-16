namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class MasseurBookedHours : BaseDeletableModel<string>
    {
        public MasseurBookedHours()
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
