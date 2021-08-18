namespace MassageStudioLorem.Tests.Controllers
{
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using MassageStudioLorem.Controllers;
    using MassageStudioLorem.Data.Models;
    using Services.Appointments.Models;
    using Models.Appointments;

    using static Data.Models.AppointmentsControllerTestModels;
    using static Data.DbModels.AppointmentsControllerTestDbModels;
    using static Global.GlobalConstants.Notifications;
    using static Global.DefaultHourSchedule;
    using static MassageStudioLorem.Areas.Client.ClientConstants;
    public class AppointmentsControllerTest
    {
        [Fact]
        public void ControllerShouldHaveAuthorizeFilter()
            => MyMvc
                .Controller<AppointmentsController>()
                .ShouldHave()
                .Attributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void IndexShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new AppointmentsListViewModel
            {
                UpcomingAppointments = new List<UpcomingAppointmentServiceModel>() { UpcomingAppointment },
                PastAppointments = new List<PastAppointmentServiceModel>() { PastAppointment }
            };

            var (pastAppointment, upcomingAppointment) = GetAppointments();

            MyController<AppointmentsController>
                .Instance()
                .WithUser(u => u.InRole(ClientRoleName))
                .WithData
                    (TestMassage,
                      TestClient,
                      TestClientUser,
                      TestMasseurUser,
                      TestMasseur,
                      pastAppointment, upcomingAppointment)
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void CancelAppointmentShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new CancelAppointmentServiceModel
            {
                Date = UpcomingAppointment.Date,
                Hour = UpcomingAppointment.Hour,
                Id = UpcomingAppointment.Id,
                MassageName = UpcomingAppointment.MassageName,
                MasseurFullName = UpcomingAppointment.MasseurFullName
            };

            var upcomingAppointment = GetUpcomingAppointment();

            MyController<AppointmentsController>
                .Instance()
                .WithData(
                  TestMasseur, 
                  TestClient, 
                  TestMassage,
                  upcomingAppointment)
                .Calling(c => c.CancelAppointment(upcomingAppointment.Id))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DeleteShouldDeleteAppointmentAndReturnRedirectWithTempData()
        {
            var upcomingAppointment = GetAppointments().upcomingAppointment;

            MyController<AppointmentsController>
                .Instance()
                .WithData(upcomingAppointment)
                .Calling(c => c.Delete(upcomingAppointment.Id))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Appointment>(set => set.ShouldBeEmpty()))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyCanceledAppointmentKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Index");
        }

        [Fact]
        public void BookShouldReturnViewWithModelWithValidData()
        {
            SeedHourScheduleAsString();

            var expectedModel = new BookAppointmentServiceModel
            {
                ClientTimeZoneOffset = 0,
                MassageId = TestMassage.Id,
                MassageName = TestMassage.Name,
                MasseurId = TestMasseur.Id,
                MasseurFullName = TestMasseur.FullName,
                WorkHours = HourScheduleAsString
            };

            MyController<AppointmentsController>
                .Instance()
                .WithData(TestMassage, TestMasseur, TestClient)
                .Calling(c => c.Book(new AppointmentIdsQueryModel()
                {
                    MassageId = TestMassage.Id,
                    MasseurId = TestMasseur.Id
                }))
                .ShouldReturn()
                .View(expectedModel);
        }
        
        [Fact]
        public void BookShouldBookAppointmentWithValidDataAndRedirectToActionWithTempData()
        {
            MyController<AppointmentsController>
                .Instance()
                .WithUser(u => u.InRole(ClientRoleName))
                .WithData
                (TestClient,
                  TestMassage,
                  TestMasseur,
                  TestMasseurUser)
                .Calling(c => c.Book(BookAppointmentData))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Appointment>(set => set.ShouldNotBeNull()))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyBookedAppointmentKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Index");
        }

    }
}
