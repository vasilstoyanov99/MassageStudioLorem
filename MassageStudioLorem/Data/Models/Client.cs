namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        public Client()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Appointments = new HashSet<Appointment>();
        }

        public string Id { get; set; }

        //public DateTime DateOfBirth { get; set; }

        // TODO: Check if this is needed
        //public string ProfileImagePath { get; set; }

        [Required] public string UserId { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}