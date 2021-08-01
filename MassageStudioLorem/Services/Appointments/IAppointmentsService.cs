namespace MassageStudioLorem.Services.Appointments
{
    using Models;
    using System;

    public interface IAppointmentsService
    {
        AppointmentServiceModel GetTheMasseurSchedule
            (string masseurId, string massageId);

        DateTime ParseDate(string dateAsString);

        string CheckIfMasseurUnavailableAndGetErrorMessage
        (DateTime date, string hour, string masseurId);

        void AddNewAppointment
        (string clientId, string masseurId, string massageId,
            DateTime date, string hour);
    }
}
