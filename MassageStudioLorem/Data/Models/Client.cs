namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Global.GlobalConstants.DataValidations;

    public class Client
    {
        public Client()
        {
            this.Id = Guid.NewGuid().ToString();
            this.TimeZoneOffset = Double.MinValue;
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required] 
        public string UserId { get; set; }

        [Required]
        public double TimeZoneOffset { get; set; }
    }
}