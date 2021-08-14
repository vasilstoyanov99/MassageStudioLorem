namespace MassageStudioLorem.Services.Appointments.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Global;
    using MassageStudioLorem.Models.CustomValidationAttributes;

    public class BookAppointmentServiceModel
    {
        public BookAppointmentServiceModel()
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

        public string ClientCurrentDateTime { get; set; }

        public IEnumerable<string> WorkHours { get; set; }
    }
}
