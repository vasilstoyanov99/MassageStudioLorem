﻿namespace MassageStudioLorem.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using MassageStudioLorem.Controllers;
    using MassageStudioLorem.Data.Models;
    using Services.Appointments.Models;
    using Models.Appointments;

    using static Data.Models.AppointmentsControllerTestModels;
    using static Data.DbModels.AppointmentsControllerTestDbModels;
    using static Areas.Client.ClientConstants;
    using static MassageStudioLorem.Global.GlobalConstants.Notifications;
    using static MassageStudioLorem.Global.DefaultHourSchedule;
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
            var model = new AppointmentsListViewModel
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
                .View(model);
        }

        [Fact]
        public void CancelAppointmentShouldReturnViewWithModelWithValidData()
        {
            var model = new CancelAppointmentServiceModel
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
                .View(model);
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
        
        //TODO: See why it doesn't wont tor run the test
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
                    .WithSet<Appointment>(set =>
                    {
                        set.ShouldNotBeNull();
                        set.FirstOrDefault(set =>
                                set.ClientPhoneNumber ==
                                ExpectedBookedAppointment.ClientPhoneNumber)
                            .ShouldNotBeNull();
                    }))
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
