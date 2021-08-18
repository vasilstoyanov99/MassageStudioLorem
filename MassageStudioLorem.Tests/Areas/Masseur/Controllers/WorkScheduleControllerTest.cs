namespace MassageStudioLorem.Tests.Areas.Masseur.Controllers
{
    using System.Collections.Generic;

    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Masseur.Controllers;
    using Services.Appointments.Models;

    using static Data.Models.WorkScheduleControllerTestModels;
    using static Data.DbModels.WorkScheduleControllerDbModels;
    using static MassageStudioLorem.Areas.Masseur.MasseurConstants;

    public class WorkScheduleControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new List<MasseurUpcomingAppointmentServiceModel>() 
                { UpcomingAppointmentModel };

            MyController<WorkScheduleController>
                .Instance()
                .WithUser(u => u.InRole(MasseurRoleName))
                .WithData
                (TestMasseur, 
                 TestClient, 
                 TestClientUser, 
                 TestUpcomingAppointment)
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(expectedModel);
        }
    }
}
