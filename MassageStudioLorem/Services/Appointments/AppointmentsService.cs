namespace MassageStudioLorem.Services.Appointments
{
    using Data;
    using Data.Models;
    using Global;
    using MassageStudioLorem.Models.Appointments;
    using Models;
    using System;
    using System.Linq;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly LoremDbContext _data;

        public AppointmentsService(LoremDbContext data) => this._data = data;

        public AppointmentServiceModel GetTheMasseurSchedule
            (string masseurId, string massageId)
        {
            if (DefaultTimeSchedule.TimeSchedule == null) DefaultTimeSchedule.SeedTimeTable();

            var massage = this.GetMassageFromDB(massageId);

            if (CheckIfNull(massage, massageId))
                return null;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (CheckIfNull(masseur, masseurId))
                return null;

            return GetAppointmentModel
                (masseurId, massageId, massage.Name, masseur.FullName);
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
    }
}
