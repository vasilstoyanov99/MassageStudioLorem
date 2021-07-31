namespace MassageStudioLorem.Services.Appointments
{
    using Models;

    public interface IAppointmentsService
    {
        AppointmentServiceModel GetTheMasseurSchedule
            (string masseurId, string massageId);
    }
}
