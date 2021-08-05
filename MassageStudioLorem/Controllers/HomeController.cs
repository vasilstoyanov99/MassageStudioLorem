namespace MassageStudioLorem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using static Areas.Client.ClientConstants;
    using static Areas.Masseur.MasseurConstants;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.User.IsInRole(ClientRoleName))
                return this.View();

            if (this.User.IsInRole(MasseurRoleName))
                return this.RedirectToAction
                ("Index", "Appointments", new {area = "Masseur"});

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Error() => this.View();
    }
}