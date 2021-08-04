namespace MassageStudioLorem.Areas.Masseur.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentsController : MasseurController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
