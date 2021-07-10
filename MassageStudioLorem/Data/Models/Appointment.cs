namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MassageStudioLorem.Data.Common.Models;

    public class Appointment : BaseDeletableModel<string>
    {
        public Appointment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string ClientId { get; set; }

        public virtual Client Client { get; set; }

        [Required] 
        public string MasseurId { get; set; }

        public virtual Masseur Masseur { get; set; }

        [Required] 
        public string MassageId { get; set; }

        public virtual Massage Massage { get; set; }

        public DateTime DateTime { get; set; }

        // The Salon can Confirm or Decline an appointment
        //TODO: Check if it's needed
        //public bool? Confirmed { get; set; }

        // For every past (and confirmed) appointment the User can Rate the Salon
        // But rating can be given only once for each appointment
        public bool? IsMasseurRatedByTheUser { get; set; }
    }
}