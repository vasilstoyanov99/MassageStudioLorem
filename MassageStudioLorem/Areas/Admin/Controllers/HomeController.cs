namespace MassageStudioLorem.Areas.Admin.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Services.Home;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomeController : AdminController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
            => this._homeService = homeService;

        public IActionResult Index()
        {
            var userId = this.User.GetId();
            ViewBag.AdminUserName = this._homeService
                .GetAdminUsername(userId);

            return this.View();
        }
    }
}
