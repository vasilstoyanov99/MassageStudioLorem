namespace MassageStudioLorem.Models.Appointments
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using CustomValidationAttributes;
    using Global;
    using static Global.DefaultHourSchedule;

    public class AppointmentFormModel
    {
        public AppointmentFormModel()
        {
            this.WorkHours = HourScheduleAsString;
        }

        public string MassageName { get; set; }

        public string MasseurFullName { get; set; }

        [Required]
        [ValidateDateString(ErrorMessage = GlobalConstants.ErrorMessages.Date)]
        public string Date { get; set; }

        [Required]
        [ValidateHourString(ErrorMessage = GlobalConstants.ErrorMessages.Hour)]
        public string Hour { get; set; }

        public string MasseurId { get; set; }

        public string MassageId { get; set; }

        public IEnumerable<string> WorkHours { get; set; }
    }
}
