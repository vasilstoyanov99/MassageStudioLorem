namespace MassageStudioLorem.Services.Appointments
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            => DateTime.Parse(dateAsString);

        public string CheckIfMasseurUnavailableAndGetErrorMessage
            (DateTime date, string hour, string masseurId)
        {
            var masseursQuery = this._data.Masseurs
                .Include(m => m.WorkSchedule).AsQueryable();

            if (DateTime.TryParse(hour, out DateTime time))
            {
               date = date.AddHours(time.Hour);

                var isUnavailable = masseursQuery.
                    Any(m => m.Id == masseurId && m.WorkSchedule
                        .Any(ws => ws.Date == date));

                if (isUnavailable)
                    return this.GetAvailableHours
                        (date, hour, masseurId, masseursQuery);

                var hoursBookedInTheDay = masseursQuery
                    .FirstOrDefault(m => m.Id == masseurId)
                    .WorkSchedule
                    .Count(ws => ws.Date.Day == date.Day && 
                                 ws.Date.Month == date.Month);

                if (hoursBookedInTheDay >= DefaultHoursPerDay)
                    return String.Format(MasseurBookedForTheDay,
                        date.ToString("dd-MM-yy"));
            }
            else
            {
                return SomethingWentWrong;
            }

            return null;
        }

        public string CheckIfClientBookedTooManyMassagesInTheSameDay
            (DateTime date, string userId)
        {
            var clientId = this.GetClientId(userId);

            var bookedMassages = this._data
                .Appointments
                .Count(a => a.ClientId == clientId && 
                            a.Date.Day == date.Day &&
                            a.Date.Month == date.Month);

            if (bookedMassages == MaxAmountToBookMassages)
                return String.Format(TooManyBookingsOfTheSameMassage,
                    MaxAmountToBookMassages);

            return null;
        }

        public bool CheckIfClientTryingToBookAPastTime
            (DateTime date, string hour)
        {
            var dateTimeNow = this.GetDateTimeNow();
            DateTime.TryParse(hour, out DateTime dateTimeHour);

            if (date.Date < dateTimeNow &&
                dateTimeHour.Hour < dateTimeNow.Hour)
                return true;

            return false;
        }

        public void AddNewAppointment
            (string userId, string masseurId, string massageId,
             DateTime date, string hour)
        {
            var dataFromDb = this.GetDataFromDB(masseurId, massageId, userId);

            var clientId = this.GetClientId(userId);
            var client = this.GetClientFromDB(clientId);

            var hourAsInt = DateTime.Parse(hour).Hour;

            var appointment = new Appointment()
            {
                Date = date.AddHours(hourAsInt),
                Hour = hour,
                ClientId = clientId,
                ClientPhoneNumber = this.GetClientPhoneNumber(clientId),
                ClientFirstName = dataFromDb.clientFirstName,
                MassageId = massageId,
                MasseurId = masseurId,
                MasseurFullName = dataFromDb.masseurFullName,
                MasseurPhoneNumber = dataFromDb.masseurPhoneNumber,
                MassageName = dataFromDb.massageName,
                IsUserReviewedMasseur = false
            };

            this._data.Appointments.Add(appointment);
            this._data.SaveChanges();
        }

        public IEnumerable<UpcomingAppointmentServiceModel> 
            GetUpcomingAppointments(string userId)
        {
            var clientId = this.GetClientId(userId);

            if (!this._data.Appointments.Any(a => a.ClientId == clientId))
                return null;

            var dateTimeNow = this.GetDateTimeNow();

            var upcomingAppointmentsModels = this._data.Appointments
                .Where(a => a.ClientId == clientId &&
                            a.Date > dateTimeNow)
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

            return upcomingAppointmentsModels;
        }

        public IEnumerable<PastAppointmentServiceModel> GetPastAppointments
            (string userId)
        {
            var dateTimeNow = this.GetDateTimeNow();
            var clientId = this.GetClientId(userId);
            
            var pastAppointmentsModels = this._data.Appointments
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

            return pastAppointmentsModels;
        }

        public IEnumerable<MasseurUpcomingAppointmentServiceModel> GetMasseurUpcomingAppointments(string userId)
        {
            var masseurId = this._data.Masseurs
                .FirstOrDefault(m => m.UserId == userId)?.Id;

            if (this.CheckIfNull(masseurId))
                return null;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur))
                return null;

            var dateTimeNow = this.GetDateTimeNow();

            var upcomingAppointmentsModels = this._data.Appointments
                .Where(a => a.MasseurId == masseurId &&
                            a.Date > dateTimeNow)
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

            return upcomingAppointmentsModels;
        }

        public CancelAppointmentServiceModel
            GetAppointment(string appointmentId)
        {
            var appointment = this.GetAppointmentFromDB(appointmentId);

            if (this.CheckIfNull(appointment))
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
                if (defaultHourSchedule.Contains(booked.Hour)) defaultHourSchedule.Remove(booked.Hour);
            }

            return
                String.Format(AvailableHoursForDate, hour,
                    date.Date.ToString("dd-MM-yy"),
                    String.Join(' ', defaultHourSchedule));
        }

        private string GetClientId(string userId) =>
            this._data.Clients.FirstOrDefault(c => c.UserId == userId)?.Id;

        private Client GetClientFromDB(string clientId) => 
            this._data.Clients.FirstOrDefault(c => c.Id == clientId);

        private string GetClientPhoneNumber(string clientId)
        {
            var clientUserId = this._data.Clients
                .FirstOrDefault(c => c.Id == clientId)?.UserId;

            return this._data.Users
                .FirstOrDefault(u => u.Id == clientUserId)?.PhoneNumber;
        }

        private (string masseurFullName, 
                 string masseurPhoneNumber,
                 string massageName,
                 string clientFirstName) 
            GetDataFromDB(string masseurId, string massageId, string userId)
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

            var client = this.GetClientFromDB(this.GetClientId(userId));

            var clientFirstName = client.FirstName;

            return (masseurData.FullName,
                masseurPhoneNumber,
                massageName, 
                clientFirstName);
        }

        private DateTime GetDateTimeNow() => DateTime.Now;
    }
}
