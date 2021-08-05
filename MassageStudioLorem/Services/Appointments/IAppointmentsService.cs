﻿namespace MassageStudioLorem.Services.Appointments
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

        bool CheckIfClientTryingToBookAPastTime(DateTime date, string hour);

        void AddNewAppointment
        (string userId, string masseurId, string massageId,
            DateTime date, string hour);

        IEnumerable<UpcomingAppointmentServiceModel> GetUpcomingAppointments
            (string userId);

        IEnumerable<PastAppointmentServiceModel> GetPastAppointments
            (string clientId);

        IEnumerable<MasseurUpcomingAppointmentServiceModel>
            GetMasseurUpcomingAppointments(string userId);

        CancelAppointmentServiceModel GetAppointment(string appointmentId);

        bool IsAppointmentDeletedSuccessful(string appointmentId);
    }
}
