namespace MassageStudioLorem.Tests.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Controllers;
    using Models.Appointments;
    using Services.Appointments.Models;

    public class AppointmentsControllerTest
    {
        [Fact]
        public void GetIndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Appointments")
                .To<AppointmentsController>(c => c.Index());

        [Fact]
        public void GetCancelAppointmentShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Appointments/CancelAppointment")
                .To<AppointmentsController>(c => c.CancelAppointment(null));

        [Fact]
        public void PostDeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Appointments/Delete")
                    .WithMethod(HttpMethod.Post))
                .To<AppointmentsController>(c => c.Delete(null));

        [Fact]
        public void GetBookShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Appointments/Book")
                .To<AppointmentsController>(c => c.Book
                    (new AppointmentIdsQueryModel()));

        [Fact]
        public void PostBookShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Appointments/Book")
                    .WithMethod(HttpMethod.Post))
                .To<AppointmentsController>(c => c.Book
                    (new BookAppointmentServiceModel()));

    }
}
