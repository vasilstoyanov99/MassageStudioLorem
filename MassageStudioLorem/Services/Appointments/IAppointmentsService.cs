namespace MassageStudioLorem.Services.Appointments
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IAppointmentsService
    {
        BookAppointmentServiceModel GetTheMasseurSchedule
            (string masseurId, string massageId);

        DateTime ParseDate(string dateAsString);

        string CheckIfMasseurUnavailableAndGetErrorMessage
        (DateTime date, string hour, string masseurId);

        string CheckIfClientBookedTooManyMassagesInTheSameDay
            (DateTime date, string userId);

        void AddNewAppointment
        (string userId, string masseurId, string massageId,
            DateTime date, string hour);

        IEnumerable<AppointmentServiceModel> GetUpcomingAppointments
            (string userId);

        CancelAppointmentServiceModel GetAppointment(string appointmentId);

        bool IsAppointmentDeletedSuccessful(string appointmentId);
    }
}
