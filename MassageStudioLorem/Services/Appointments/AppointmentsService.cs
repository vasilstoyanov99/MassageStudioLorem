namespace MassageStudioLorem.Services.Appointments
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using static Global.GlobalConstants.DataValidations;
    using static Global.GlobalConstants.ErrorMessages;
    using static Global.DefaultHourSchedule;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly LoremDbContext _data;

        public AppointmentsService(LoremDbContext data) => this._data = data;

        public BookAppointmentServiceModel GetTheMasseurSchedule
            (string masseurId, string massageId)
        {
            if (this.CheckIfNull(HourScheduleAsString))
                SeedHourScheduleAsString();

            var massage = this.GetMassageFromDB(massageId);

            if (this.CheckIfNull(massage))
                return null;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur))
                return null;

            return this.GetAppointmentModel
                (masseurId, massageId, massage.Name, masseur.FullName);
        }

        public DateTime ParseDate(string dateAsString)
        {
            //TODO: Decided if this if is need

            if (!DateTime.TryParse(dateAsString, out DateTime date))
                return DateTime.MaxValue;

            return date;
        }

        public string CheckIfMasseurUnavailableAndGetErrorMessage
            (DateTime date, string hour, string masseurId)
        {
            var masseursQuery = this._data.Masseurs
                .Include(m => m.WorkSchedule).AsQueryable();

            var isUnavailable = masseursQuery.
                    Any(m => m.Id == masseurId && m.WorkSchedule.Any(ws => ws.Date == date && ws.Hour == hour));

            if (isUnavailable)
            {
                var hoursBookedInTheDay = masseursQuery
                    .FirstOrDefault(m => m.Id == masseurId)
                    .WorkSchedule
                    .Count(ws => ws.Date == date && ws.Hour == hour);

                if (hoursBookedInTheDay == DefaultHoursPerDay)
                {
                    return String.Format(MasseurBookedForTheDay,
                        date.ToString("dd-MM-yy"));
                }

                return this.GetAvailableHours
                    (date, hour, masseurId, masseursQuery);
            }

            return null;
        }

        public string CheckIfClientBookedTooManyMassagesInTheSameDay
            (DateTime date, string userId)
        {
            var clientId = this.GetClientId(userId);

            var bookedMassages = this._data
                .Appointments
                .Count(a => a.ClientId == clientId && a.Date == date);

            if (bookedMassages == MaxAmountToBookMassages)
                return String.Format(TooManyBookingsOfTheSameMassage,
                    MaxAmountToBookMassages);

            return null;
        }

        public void AddNewAppointment
            (string userId, string masseurId, string massageId,
             DateTime date, string hour)
        {
            var dataFromDb = this.GetMasseurNameAndNumber(masseurId, massageId);

            var clientId = this.GetClientId(userId);

            var appointment = new Appointment()
            {
                Date = date,
                Hour = hour,
                ClientId = clientId,
                MassageId = massageId,
                MasseurId = masseurId,
                MasseurFullName = dataFromDb.masseurFullName,
                MasseurPhoneNumber = dataFromDb.masseurPhoneNumber,
                MassageName = dataFromDb.massageName,
                IsUserReviewedMasseur = false
            };

            this._data.Appointments.Add(appointment);
            var masseur = this.GetMasseurFromDB(masseurId);
            masseur.WorkSchedule.Add(appointment);
            var client = this._data.Clients
                .FirstOrDefault(c => c.UserId == userId);
            client.Appointments.Add(appointment);
            this._data.SaveChanges();
        }

        public IEnumerable<AppointmentServiceModel> 
            GetUpcomingAppointments(string userId)
        {
            var clientId = this.GetClientId(userId);

            if (!this._data.Appointments.Any(a => a.ClientId == clientId))
                return null;

            var appointmentsModels = this._data.Appointments
                .Where(a => a.ClientId == clientId && a.Date > DateTime.UtcNow)
                .Select(a => new AppointmentServiceModel()
                {
                    Id = a.Id,
                    Date = a.Date,
                    Hour = a.Hour,
                    MassageName = a.MassageName,
                    MasseurFullName = a.MasseurFullName,
                    MasseurPhoneNumber = a.MasseurPhoneNumber,
                    IsUserReviewedMasseur = a.IsUserReviewedMasseur,
                    //TODO: Add masseurPhoneNumber
                })
                .OrderBy(a => a.Date)
                .ToList();

            return appointmentsModels;
        }

        public CancelAppointmentServiceModel GetAppointment(string appointmentId)
        {
            var appointment = this.GetAppointmentFromDB(appointmentId);

            if (CheckIfNull(appointment))
                return null;

            var cancelAppointmentModel = new CancelAppointmentServiceModel()
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Hour = appointment.Hour,
                MassageName = appointment.MassageName,
                MasseurFullName = appointment.MasseurFullName
            };

            return cancelAppointmentModel;
        }

        public bool IsAppointmentDeletedSuccessful
            (string appointmentId)
        {
            var appointment = this.GetAppointmentFromDB(appointmentId);

            if (this.CheckIfNull(appointment))
                return false;

            this._data.Appointments.Remove(appointment);
            this._data.SaveChanges();
            return true;
        }

        private bool CheckIfNull(object obj)
            => obj == null;

        private Masseur GetMasseurFromDB(string masseurId) =>
            this._data
                .Masseurs
                .FirstOrDefault(m => m.Id == masseurId);

        private Massage GetMassageFromDB(string massageId) =>
            this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

        private Appointment GetAppointmentFromDB(string appointmentId) =>
            this._data
                .Appointments
                .FirstOrDefault(a => a.Id == appointmentId);

        private BookAppointmentServiceModel GetAppointmentModel
            (string masseurId, string massageId, 
            string massageName, string masseurFullName)
            => new()
            {
                MassageName = massageName,
                MasseurFullName = masseurFullName,
                MassageId = massageId,
                MasseurId = masseurId
            };

        private string GetAvailableHours
            (DateTime date, string hour, string masseurId,
            IQueryable<Masseur> masseursQuery)
        {
            var bookedHours = masseursQuery
                .FirstOrDefault(m => m.Id == masseurId)
                .WorkSchedule.Where(ws => ws.Date == date)
                .Select(s => new {s.Hour})
                .ToList();

            if (this.CheckIfNull(HourScheduleAsString))
                SeedHourScheduleAsString();

            var defaultHourSchedule = new List<string>(HourScheduleAsString);

            foreach (var booked in bookedHours)
            {
                if (defaultHourSchedule.Contains(booked.Hour))
                {
                    defaultHourSchedule.Remove(booked.Hour);
                }
            }

            return
                String.Format(AvailableHoursForDate, hour,
                    date.Date.ToString("dd-MM-yy"),
                    String.Join(' ', defaultHourSchedule));
        }

        private string GetClientId(string userId) =>
            this._data.Clients.FirstOrDefault(c => c.UserId == userId).Id;

        private (string masseurFullName, 
                 string masseurPhoneNumber,
                 string massageName) 
            GetMasseurNameAndNumber(string masseurId, string massageId)
        {
            var masseurData = this._data.Masseurs
                .Where(m => m.Id == masseurId)
                .Select(m => new {m.FullName, m.UserId})
                .FirstOrDefault();

            var masseurPhoneNumber = 
                this._data.Users
                    .FirstOrDefault(u => u.Id == masseurData.UserId)?.PhoneNumber;

            var massageName = this._data.Massages
                .FirstOrDefault(m => m.Id == massageId)?.Name;

            return (masseurData.FullName, masseurPhoneNumber, massageName);
        }
    }
}
