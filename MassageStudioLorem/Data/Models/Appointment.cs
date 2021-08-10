namespace MassageStudioLorem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Appointment
    {
        public Appointment() => this.Id = Guid.NewGuid().ToString();

        public string Id { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientPhoneNumber { get; set; }

        [Required]
        public string ClientFirstName { get; set; }

        [Required] 
        public string MasseurId { get; set; }

        [Required]
        public string MasseurFullName { get; set; }

        [Required]
        public string MasseurPhoneNumber { get; set; }

        [Required]
        public string MassageId { get; set; }

        [Required]
        public string MassageName { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Hour { get; set; }

        public bool IsUserReviewedMasseur { get; set; }
    }
}