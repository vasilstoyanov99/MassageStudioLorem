namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using System.ComponentModel.DataAnnotations;

    public class Masseur
    {
        public Masseur()
        {
            this.Id = Guid.NewGuid().ToString();
            //Rating = new List<double>();
            this.WorkSchedule = new HashSet<Appointment>();
            this.Comments = new HashSet<Comment>();
            this.Massages = new HashSet<Massage>();
        }

        public string Id { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string MiddleName { get; set; }

        [Required] public string LastName { get; set; }

        //public DateTime DateOfBirth { get; set; }

        //TODO: I can add a sorting feature by gender!
        //Gender will be enum ->  Male = 1,
        //Female = 2, 
        //public Gender Gender { get; set; }

        [Required] public string ProfileImagePath { get; set; }

        [Required] public string Description { get; set; }

        [Required] public string PhoneNumber { get; set; }


        // TODO: Check if Rating should be double
        public double Rating { get; set; }

        public int RatersCount { get; set; }

        [Required] public string UserId { get; set; }

        //public virtual ICollection<MasseurBookedHours>
        //    BookedHours { get; set; }

        public ICollection<Appointment> WorkSchedule { get; set; }

        public ICollection<Comment> Comments { get; set; }

        // A collection of massages that the masseur can do
        public ICollection<Massage> Massages { get; set; }
    }
}