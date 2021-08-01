namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Appointment
    {
        public Appointment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string ClientId { get; set; }

        //public Client Client { get; set; }

        [Required] 
        public string MasseurId { get; set; }

        //public Masseur Masseur { get; set; }
        public string MassageId { get; set; }

        //public Massage Massage { get; set; }

        public DateTime Date { get; set; }

        public string Hour { get; set; }

        // The Salon can Confirm or Decline an appointment
        //TODO: Check if it's needed
        //public bool? Confirmed { get; set; }

        // For every past (and confirmed) appointment the User can Rate the Salon
        // But rating can be given only once for each appointment
        public bool? IsMasseurRatedByTheUser { get; set; }
    }
}