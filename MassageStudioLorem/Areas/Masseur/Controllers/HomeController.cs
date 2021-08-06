namespace MassageStudioLorem.Areas.Masseur.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Services.Home;

    public class HomeController : MasseurController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
            => this._homeService = homeService;

        public IActionResult Index()
        {
            var userId = this.User.GetId();
            ViewBag.MasseurFullName = this._homeService
                .GetMasseurFullName(userId);

            return this.View();
        }
    }
}
