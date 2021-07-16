namespace MassageStudioLorem.Models.Appointments
{
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
        public string MasseurId { get; init; }

        [Required]
        public int MassageId { get; init; }

        [Required]
        //[ValidateDateString(ErrorMessage = GlobalConstants.ErrorMessages.DateTime)]
        public string Date { get; init; }

        [Required]
        //[ValidateTimeString(ErrorMessage = GlobalConstants.ErrorMessages.DateTime)]
        public string Hour { get; init; }

        public ICollection<string> WorkHours { get; set; }
    }
}
