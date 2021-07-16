namespace MassageStudioLorem.Models.Appointments
{
    using CustomValidationAttributes;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using Global;

    public class AppointmentInputModel
    {
        public AppointmentInputModel()
        {
            this.WorkHours = DefaultTimeSchedule.TimeSchedule;
        }

        [Required]
        public string MasseurId { get; set; }

        [Required]
        public int MassageId { get; set; }

        [Required]
        [ValidateDateString(ErrorMessage = GlobalConstants.ErrorMessages.Date)]
        public string Date { get; set; }

        [Required]
        [ValidateHourString(ErrorMessage = GlobalConstants.ErrorMessages.Hour)]
        public string Hour { get; set; }

        public ICollection<string> WorkHours { get; set; }
    }
}
