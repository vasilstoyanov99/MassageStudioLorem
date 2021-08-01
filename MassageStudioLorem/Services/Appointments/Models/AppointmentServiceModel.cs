namespace MassageStudioLorem.Services.Appointments.Models
{
    using Global;
    using MassageStudioLorem.Models.CustomValidationAttributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AppointmentServiceModel
    {
        public AppointmentServiceModel()
            => this.WorkHours = DefaultHourSchedule.HourScheduleAsString;

        public string MassageName { get; set; }

        public string MasseurFullName { get; set; }

        [Required]
        [ValidateDateString(ErrorMessage = GlobalConstants.ErrorMessages.Date)]
        public string Date { get; set; }

        [Required]
        [ValidateHourString(ErrorMessage = GlobalConstants.ErrorMessages.Hour)]
        public string Hour { get; set; }

        [Required]
        public string MasseurId { get; set; }

        [Required]
        public string MassageId { get; set; }

        public IEnumerable<string> WorkHours { get; set; }
    }
}
