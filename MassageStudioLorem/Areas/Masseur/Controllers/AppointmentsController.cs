namespace MassageStudioLorem.Areas.Masseur.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmentsController : MasseurController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
