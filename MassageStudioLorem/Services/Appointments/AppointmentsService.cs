namespace MassageStudioLorem.Services.Appointments
{
    using Data;
    using Data.Models;
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
            var appointmentsQuery = this._data.Appointments.AsQueryable();

            var isUnavailable = appointmentsQuery
                    .Any(a => a.MasseurId == masseurId && 
                              a.Date == date && a.Hour == hour);

            if (isUnavailable)
            {
                var hoursBookedInTheDay = appointmentsQuery
                    .Count(x => x.MasseurId == masseurId && x.Date == date);

                if (hoursBookedInTheDay == DefaultHoursPerDay)
                {
                    return String.Format(MasseurBookedForTheDay,
                        date.ToString("dd-mm-yyyy"));
                }

                return this.GetAvailableHours
                    (date, hour, masseurId, appointmentsQuery);
            }

            return null;
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
            IQueryable<Appointment> appointments)
        {
            var bookedHours = appointments.
                Where(x => x.MasseurId == masseurId &&
                            x.Date == date)
                .Select(x => new { x.Hour })
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
