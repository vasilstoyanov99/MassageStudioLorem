namespace MassageStudioLorem.Services.Appointments
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;
    using System.Text;
    using static Global.GlobalConstants.DataValidations;
    using static Global.GlobalConstants.ErrorMessages;
    using static Global.DefaultHourSchedule;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly LoremDbContext _data;

        public AppointmentsService(LoremDbContext data) => this._data = data;

        public AppointmentServiceModel GetTheMasseurSchedule
            (string masseurId, string massageId)
        {
            if (HourScheduleAsString == null)
                SeedHourScheduleAsString();

            var massage = this.GetMassageFromDB(massageId);

            if (this.CheckIfNull(massage, massageId))
                return null;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur, masseurId))
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
                    Any(m => m.UserId == masseurId && m.WorkSchedule.Any(ws => ws.Date == date && ws.Hour == hour));

            if (isUnavailable)
            {
                var hoursBookedInTheDay = masseursQuery
                    .FirstOrDefault(m => m.UserId == masseurId)
                    .WorkSchedule
                    .Count(ws => ws.Date == date && ws.Hour == hour);

                if (hoursBookedInTheDay == DefaultHoursPerDay)
                {
                    return String.Format(MasseurBookedForTheDay,
                        date.ToString("dd-mm-yyyy"));
                }

                return this.GetAvailableHours
                    (date, hour, masseurId, masseursQuery);
            }

            return null;
        }

        public void AddNewAppointment
            (string clientId, string masseurId, string massageId,
             DateTime date, string hour)
        {
            var appointment = new Appointment()
            {
                ClientId = clientId,
                MasseurId = masseurId,
                MassageId = massageId,
                Date = date,
                Hour = hour,
                IsMasseurRatedByTheUser = false
            };

            this._data.Appointments.Add(appointment);
            var masseur = GetMasseurFromDB(masseurId);
            masseur.WorkSchedule.Add(appointment);
            var client = this._data.Clients
                .FirstOrDefault(c => c.UserId == clientId);
            client.Appointments.Add(appointment);
            this._data.SaveChanges();
        }

        private bool CheckIfNull(object massage, string id)
            => String.IsNullOrEmpty(id) || massage == null;

        private Masseur GetMasseurFromDB(string masseurId) =>
            this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == masseurId);

        private Massage GetMassageFromDB(string massageId) =>
            this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

        private AppointmentServiceModel GetAppointmentModel
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
                .FirstOrDefault(m => m.UserId == masseurId)
                .WorkSchedule.Where(ws => ws.Date == date)
                .Select(s => new {s.Hour})
                .ToList();

            if (HourScheduleAsString == null)
                SeedHourScheduleAsString();

            var defaultHourSchedule = HourScheduleAsString;

            foreach (var booked in bookedHours)
            {
                if (defaultHourSchedule.Contains(booked.Hour))
                {
                    defaultHourSchedule.Remove(booked.Hour);
                }
            }

            return
                String.Format(AvailableHoursForDate, hour, date,
                    String.Join(' ', defaultHourSchedule));
        }
    }
}
