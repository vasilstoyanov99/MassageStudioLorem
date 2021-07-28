namespace MassageStudioLorem.Data.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Global.GlobalConstants.DataValidations;

    public class Masseur
    {
        public Masseur()
        {
            this.Id = Guid.NewGuid().ToString();
            this.WorkSchedule = new HashSet<Appointment>();
            this.Comments = new HashSet<Comment>();
            //this.Massages = new HashSet<Massage>();
        }

        public string Id { get; set; }

        [Required] 
        [MaxLength(FullNameMaxLength)] 
        public string FullName { get; set; }

        //public DateTime DateOfBirth { get; set; }

        //TODO: I can add a sorting feature by gender!
        public Gender Gender { get; set; }

        [Required]
        [RegularExpression(UrlRegex)]
        public string ProfileImageUrl { get; set; }

        [Required]
        [MaxLength(MasseurDescriptionMaxLength)]
        public string Description { get; set; }

        //[Required] 
        //[Phone]
        //public string PhoneNumber { get; set; }


        // TODO: Check if Rating should be double
        public double Rating { get; set; }

        public int RatersCount { get; set; }

        [Required] public string UserId { get; set; }

        [Required] public string CategoryId { get; set; }

        public Category Category { get; set; }

        //public virtual ICollection<MasseurBookedHours>
        //    BookedHours { get; set; }

        public ICollection<Appointment> WorkSchedule { get; set; }

        public ICollection<Comment> Comments { get; set; }

        // A collection of massages that the masseur can do
        //public ICollection<Massage> Massages { get; set; }
    }
}