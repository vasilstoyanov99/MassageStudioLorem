namespace MassageStudioLorem.Services.Appointments
{
    using Models;
    using System;

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
        (string clientId, string masseurId, string massageId,
            DateTime date, string hour);
    }
}
