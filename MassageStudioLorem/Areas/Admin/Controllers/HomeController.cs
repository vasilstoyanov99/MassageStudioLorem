namespace MassageStudioLorem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using MassageStudioLorem.Services.Home;

    public class HomeController : AdminController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
            => this._homeService = homeService;

        public IActionResult Index()
        {
            var userId = this.User.GetId();
            this.ViewBag.AdminUserName = this._homeService
                .GetAdminUsername(userId);

            return this.View();
        }
    }
}
