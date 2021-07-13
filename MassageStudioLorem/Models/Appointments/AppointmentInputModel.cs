namespace MassageStudioLorem.Models.Appointments
{
    using System.ComponentModel.DataAnnotations;

    using Global;

    public class AppointmentInputModel
    {
        [Required]
        public string SalonId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        //[ValidateDateString(ErrorMessage = GlobalConstants.ErrorMessages.DateTime)]
        public string Date { get; set; }

        [Required]
        //[ValidateTimeString(ErrorMessage = GlobalConstants.ErrorMessages.DateTime)]
        public string Time { get; set; }
    }
}
