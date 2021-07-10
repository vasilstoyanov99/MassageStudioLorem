namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MassageStudioLorem.Data.Common.Models;

    public class Client : BaseDeletableModel<string>
    {
        public Client()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Appointments = new HashSet<Appointment>();
        }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        // TODO: Check if this is needed
        public string ProfileImagePath { get; set; }

        [Required] public string PhoneNumber { get; set; }

        [Required] public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}