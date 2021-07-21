namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Global.GlobalConstants.DataValidations;

    public class Client
    {
        public Client()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Appointments = new HashSet<Appointment>();
        }

        public string Id { get; set; }

        [Required] 
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required] 
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        //public DateTime DateOfBirth { get; set; }

        // TODO: Check if this is needed
        //public string ProfileImagePath { get; set; }

        [Required] 
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}