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
    }
}
