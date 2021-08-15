namespace MassageStudioLorem.Services.Appointments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Global;
    using Models;

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
            if (CheckIfNull(HourScheduleAsString))
                SeedHourScheduleAsString();

            var massage = this.GetMassageFromDB(massageId);

            if (CheckIfNull(massage))
                return null;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (CheckIfNull(masseur))
                return null;

            return GetAppointmentModel
                (masseurId, massageId, massage.Name, masseur.FullName);
        }

        public DateTime TryToParseDate(string dateAsString, string hourAsString)
        {
            var cultureInfo = CultureInfo.GetCultureInfo("bg-BG");

            if (!DateTime.TryParseExact(hourAsString, GlobalConstants.DateTimeFormats.HourFormat, cultureInfo, DateTimeStyles.None, out DateTime parsedTime))
                return DateTime.MinValue;

            if (!DateTime.TryParseExact(dateAsString, GlobalConstants.DateTimeFormats.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parseDate))
                return DateTime.MinValue;

            //TODO: Make it bool
            return parseDate.AddHours(parsedTime.Hour);
        }

        public string CheckIfMasseurUnavailableAndGetErrorMessage
            (DateTime appointmentDateTime, string hourAsString, string masseurId)
        {
            var cultureInfo = CultureInfo.GetCultureInfo("bg-BG");
            if (DateTime.TryParseExact
                (hourAsString, GlobalConstants.DateTimeFormats.HourFormat,
                    cultureInfo, DateTimeStyles.None, out _))
            {
                var masseursQuery = this._data.Masseurs
                    .Include(m => m.WorkSchedule).AsQueryable();
                var isUnavailable = masseursQuery.
                    Any(m => m.Id == masseurId && m.WorkSchedule
                        .Any(ws => ws.Date == appointmentDateTime));

                if (isUnavailable)
                    return GetAvailableHours
                        (appointmentDateTime, hourAsString, 
                            masseurId, masseursQuery);

                var hoursBookedInTheDay = masseursQuery
                    .FirstOrDefault(m => m.Id == masseurId)
                    ?.WorkSchedule
                    .Count(ws => ws.Date.Day == appointmentDateTime.Day && 
                                 ws.Date.Month == appointmentDateTime.Month &&
                                 ws.Date.Year == appointmentDateTime.Year);

                if (hoursBookedInTheDay >= DefaultHoursPerDay)
                    return String.Format(MasseurBookedForTheDay,
                        appointmentDateTime.ToString("dd-MM-yy"));
            }
            else
            {
                return SomethingWentWrong;
            }

            return null;
        }

        public string CheckIfClientBookedTooManyMassagesInTheSameDay
            (DateTime appointmentDateTime, string userId)
        {
            var clientId = this.GetClientId(userId);

            var bookedMassages = this._data
                .Appointments
                .Count(a => a.ClientId == clientId && 
                            a.Date.Day == appointmentDateTime.Day &&
                            a.Date.Month == appointmentDateTime.Month &&
                            a.Date.Year == appointmentDateTime.Year);

            if (bookedMassages == MaxAmountToBookMassages)
                return String.Format(TooManyBookingsOfTheSameMassage,
                    MaxAmountToBookMassages);

            return null;
        }

        public bool CheckIfClientTryingToBookAPastTime
            (DateTime clientCurrentDateTime, DateTime appointmentDateTime)
        {
            if (clientCurrentDateTime > appointmentDateTime)
                return true;

            return false;
        }

        public void AddNewAppointment
            (string userId, 
             string masseurId, 
             string massageId,
             DateTime appointmentDateTime, 
             string hourAsString, 
             double clientTimeZoneOffset)
        {
            var dataFromDb = this.GetDataFromDB(masseurId, massageId, userId);

            var appointment = new Appointment()
            {
                Date = appointmentDateTime,
                Hour = hourAsString,
                ClientId = dataFromDb.clientId,
                ClientPhoneNumber = this.GetClientPhoneNumber(dataFromDb.clientId),
                ClientFirstName = dataFromDb.clientFirstName,
                MassageId = massageId,
                MasseurId = masseurId,
                MasseurFullName = dataFromDb.masseurFullName,
                MasseurPhoneNumber = dataFromDb.masseurPhoneNumber,
                MassageName = dataFromDb.massageName,
                IsUserReviewedMasseur = false
            };

            this._data.Appointments.Add(appointment);

            var client = this.GetClientFromDB(dataFromDb.clientId);

            if (client.TimeZoneOffset == Double.MinValue)
            {
                var reversedOffset = clientTimeZoneOffset * -1.00;
                client.TimeZoneOffset = reversedOffset;
            }

            this._data.SaveChanges();
        }

        public IEnumerable<UpcomingAppointmentServiceModel> 
            GetUpcomingAppointments(string userId)
        {
            var clientId = this.GetClientId(userId);

            if (!this._data.Appointments.Any(a => a.ClientId == clientId))
                return null;

            var clientCurrentDateTime = this.GetClientCurrentDateTime(clientId);

            var upcomingAppointmentsModels = this.GetUpcomingAppointments
                (clientId, clientCurrentDateTime);

            return upcomingAppointmentsModels;
        }

        public IEnumerable<PastAppointmentServiceModel> GetPastAppointments
            (string userId)
        {
            var clientId = this.GetClientId(userId);
            var dateTimeNow = this.GetClientCurrentDateTime(clientId);

            var pastAppointmentsModels = this.GetPastAppointmentsModels
                (clientId, dateTimeNow);

            return pastAppointmentsModels;
        }

        public IEnumerable<MasseurUpcomingAppointmentServiceModel> 
            GetMasseurUpcomingAppointments(string userId)
        {
            var masseurId = this._data.Masseurs
                .FirstOrDefault(m => m.UserId == userId)?.Id;

            if (CheckIfNull(masseurId))
                return null;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (CheckIfNull(masseur))
                return null;

            // We assume that both the clients and masseurs live in the same time zone.
            var appointment = this._data.Appointments.First();
            var clientTimeZoneOffset = this._data.Clients
                .FirstOrDefault(c => c.Id == appointment.ClientId).TimeZoneOffset;
            var currentDateTime = GetCurrentDateTime(clientTimeZoneOffset);

            var upcomingAppointmentsModels = this.GetMasseurUpcomingAppointmentsModels
                (masseurId, currentDateTime);

            return upcomingAppointmentsModels;
        }

        public CancelAppointmentServiceModel
            GetAppointment(string appointmentId)
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

        public bool CheckIfAppointmentIsDeletedSuccessfully
            (string appointmentId)
        {
            var appointment = this.GetAppointmentFromDB(appointmentId);

            if (CheckIfNull(appointment))
                return false;

            this._data.Appointments.Remove(appointment);
            this._data.SaveChanges();
            return true;
        }

        private static bool CheckIfNull(object obj)
            => obj == null;

        private static BookAppointmentServiceModel GetAppointmentModel
        (string masseurId, string massageId,
            string massageName, string masseurFullName)
            => new()
            {
                MassageName = massageName,
                MasseurFullName = masseurFullName,
                MassageId = massageId,
                MasseurId = masseurId
            };

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

        private Client GetClientFromDB(string clientId) =>
            this._data.Clients.FirstOrDefault(c => c.Id == clientId);

        private string GetClientPhoneNumber(string clientId)
        {
            var clientUserId = this._data.Clients
                .FirstOrDefault(c => c.Id == clientId)?.UserId;

            return this._data.Users
                .FirstOrDefault(u => u.Id == clientUserId)?.PhoneNumber;
        }

        private DateTime GetClientCurrentDateTime(string clientId)
        {
            var clientTimeZoneOffset = this.GetClientTimeZoneOffset(clientId);
            var clientCurrentDateTime =
                GetCurrentDateTime(clientTimeZoneOffset);

            return clientCurrentDateTime;
        }

        private double GetClientTimeZoneOffset(string clientId)
            => this._data
                .Clients
                .FirstOrDefault(c => c.Id == clientId)
                .TimeZoneOffset;

            private (string masseurFullName,
            string masseurPhoneNumber,
            string massageName,
            string clientFirstName,
            string clientId)
            GetDataFromDB(string masseurId, string massageId, string userId)
        {
            var masseurData = this._data.Masseurs
                .Where(m => m.Id == masseurId)
                .Select(m => new { m.FullName, m.UserId })
                .FirstOrDefault();

            var masseurPhoneNumber =
                this._data.Users
                    .FirstOrDefault(u => u.Id == masseurData.UserId)?.PhoneNumber;

            var massageName = this._data.Massages
                .FirstOrDefault(m => m.Id == massageId)?.Name;

            var client = this.GetClientFromDB(this.GetClientId(userId));

            var clientFirstName = client.FirstName;

            return (masseurData.FullName,
                masseurPhoneNumber,
                massageName,
                clientFirstName,
                client.Id);
        }

        private static string GetAvailableHours
            (DateTime appointmentDateTime, 
             string appointmentHour, 
             string masseurId,
            IQueryable<Masseur> masseursQuery)
        {
            var bookedHours = masseursQuery
                .FirstOrDefault(m => m.Id == masseurId)
                ?.WorkSchedule
                .Where(ws => ws.Date.Day == appointmentDateTime.Day && 
                             ws.Date.Month == appointmentDateTime.Month && 
                             ws.Date.Year == appointmentDateTime.Year)
                .Select(s => new {s.Hour})
                .ToList();

            if (CheckIfNull(HourScheduleAsString))
                SeedHourScheduleAsString();

            var defaultHourSchedule = new List<string>(HourScheduleAsString);

            foreach (var booked in bookedHours)
            {
                if (defaultHourSchedule.Contains(booked.Hour)) defaultHourSchedule.Remove(booked.Hour);
            }

            return
                String.Format(AvailableHoursForDate, appointmentHour,
                    appointmentDateTime.Date.ToString("dd-MM-yy"),
                    String.Join(' ', defaultHourSchedule));
        }

        private IEnumerable<MasseurUpcomingAppointmentServiceModel>
            GetMasseurUpcomingAppointmentsModels
            (string masseurId, DateTime currentDateTime)
            => this._data.Appointments
                .Where(a => a.MasseurId == masseurId &&
                            a.Date > currentDateTime)
                .Select(a => new MasseurUpcomingAppointmentServiceModel()
                {
                    Date = a.Date,
                    Hour = a.Hour,
                    MassageName = a.MassageName,
                    ClientPhoneNumber = a.ClientPhoneNumber,
                    ClientFirstName = a.ClientFirstName
                })
                .OrderBy(a => a.Date)
                .ToList();

        private IEnumerable<UpcomingAppointmentServiceModel>
            GetUpcomingAppointments(string clientId, DateTime clientCurrentDateTime)
            => this._data.Appointments
                .Where(a => a.ClientId == clientId &&
                            a.Date > clientCurrentDateTime)
                .Select(a => new UpcomingAppointmentServiceModel()
                {
                    Id = a.Id,
                    Date = a.Date,
                    Hour = a.Hour,
                    MassageName = a.MassageName,
                    MasseurFullName = a.MasseurFullName,
                    MasseurPhoneNumber = a.MasseurPhoneNumber
                })
                .OrderBy(a => a.Date)
                .ToList();

        private IEnumerable<PastAppointmentServiceModel>
            GetPastAppointmentsModels(string clientId, DateTime dateTimeNow)
            => this._data.Appointments
               .Where(a => a.ClientId == clientId &&
                           a.Date < dateTimeNow)
               .Select(a => new PastAppointmentServiceModel()
               {
                   Id = a.Id,
                   Date = a.Date,
                   Hour = a.Hour,
                   MasseurId = a.MasseurId,
                   MassageName = a.MassageName,
                   MasseurFullName = a.MasseurFullName,
                   MasseurPhoneNumber = a.MasseurPhoneNumber,
                   ClientId = clientId,
                   IsUserReviewedMasseur = a.IsUserReviewedMasseur
               })
               .OrderByDescending(a => a.Date)
               .ToList();

        private string GetClientId(string userId) =>
            this._data.Clients.FirstOrDefault(c => c.UserId == userId)?.Id;

        private static DateTime GetCurrentDateTime(double timeZoneOffset)
            => DateTime.UtcNow.AddHours(timeZoneOffset);
    }
}
