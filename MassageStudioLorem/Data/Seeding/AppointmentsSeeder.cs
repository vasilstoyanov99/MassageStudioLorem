namespace MassageStudioLorem.Data.Seeding
{
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class AppointmentsSeeder : ISeeder
    {
        public void Seed(LoremDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Appointments.Any())
            {
                var client = data.Clients.First();
                var clientPhoneNumber = data.Users
                    .FirstOrDefault(u => u.Id == client.UserId)?.PhoneNumber;
                var thaiMassage = data.Massages
                    .FirstOrDefault(m => m.Name == MassagesSeedData.Thai.Name);
                var masseurNatalie = data.Masseurs
                    .FirstOrDefault(m => m.FullName == "Natalie Armstrong");
                var nataliePhoneNumber = GetMasseurPhoneNumber
                    (masseurNatalie.UserId, data);

                SeedUpcomingAppointments(client, clientPhoneNumber, thaiMassage,
                    masseurNatalie, nataliePhoneNumber, data);

                var triggerPointMassage = data.Massages
                    .FirstOrDefault
                        (m => m.Name == MassagesSeedData.TriggerPoint.Name);
                var masseurBrayden = data.Masseurs
                    .FirstOrDefault(m => m.FullName == "Brayden Ford");
                var braydenPhoneNumber = GetMasseurPhoneNumber
                    (masseurBrayden.UserId, data);
                SeedPastAppointments(client, clientPhoneNumber, triggerPointMassage,
                    masseurBrayden, braydenPhoneNumber, data);

                data.SaveChanges();
            }
        }

        private static void SeedUpcomingAppointments
        (Client client,
            string clientPhoneNumber, 
            Massage massage, 
            Masseur masseur, 
            string masseurPhoneNumber,
            LoremDbContext data)
        {
            for (int i = 0; i < 4; i++)
            {
                var appointment = new Appointment()
                {
                    ClientFirstName = client.FirstName,
                    ClientId = client.Id,
                    ClientPhoneNumber = clientPhoneNumber,
                    Date = DateTime.Now.AddDays(2),
                    Hour = "1" + $"{i}:00",
                    IsUserReviewedMasseur = false,
                    MassageId = massage.Id,
                    MassageName = massage.Name,
                    MasseurFullName = masseur.FullName,
                    MasseurId = masseur.Id,
                    MasseurPhoneNumber = masseurPhoneNumber
                };

                data.Appointments.Add(appointment);
            }
        }

        private static void SeedPastAppointments
        (Client client,
            string clientPhoneNumber,
            Massage massage,
            Masseur masseur,
            string masseurPhoneNumber,
            LoremDbContext data)
        {
            for (int i = 0; i < 4; i++)
            {
                var appointment = new Appointment()
                {
                    ClientFirstName = client.FirstName,
                    ClientId = client.Id,
                    ClientPhoneNumber = clientPhoneNumber,
                    Date = DateTime.Now.AddDays(-2),
                    Hour = "1" + $"{i}:00",
                    IsUserReviewedMasseur = false,
                    MassageId = massage.Id,
                    MassageName = massage.Name,
                    MasseurFullName = masseur.FullName,
                    MasseurId = masseur.Id,
                    MasseurPhoneNumber = masseurPhoneNumber
                };

                data.Appointments.Add(appointment);
            }
        }

        private static string GetMasseurPhoneNumber(string userId, LoremDbContext data)   
        => data.Users
            .FirstOrDefault(u => u.Id == userId)?.PhoneNumber;
    }
}
