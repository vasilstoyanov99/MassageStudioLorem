namespace MassageStudioLorem.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Services.Home;
    using static Areas.Client.ClientConstants;
    using static Areas.Masseur.MasseurConstants;
    using static Areas.Admin.AdminConstants;

    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
            => this._homeService = homeService;
        
        public IActionResult Index()
        {
            if (this.User.IsInRole(ClientRoleName))
            {
                var userId = this.User.GetId();
                ViewBag.ClientFirstName = this._homeService
                    .GetClientFirstName(userId);
                return this.View();
            }

            if (this.User.IsInRole(MasseurRoleName))
                return this.RedirectToAction("Index", "Home",
                    new {area = MasseurAreaName});

            if (this.User.IsInRole(AdminRoleName))
                return this.RedirectToAction("Index", "Home",
                    new { area = AdminRoleName });

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Error() => this.View();
    }
}