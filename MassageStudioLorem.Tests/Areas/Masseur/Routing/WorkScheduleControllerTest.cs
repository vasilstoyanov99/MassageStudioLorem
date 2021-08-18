namespace MassageStudioLorem.Tests.Areas.Masseur.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Masseur.Controllers;

    public class WorkScheduleControllerTest
    {
        [Fact]
        public void GetIndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Masseur/WorkSchedule")
                .To<WorkScheduleController>(c => c.Index());
    }
}
