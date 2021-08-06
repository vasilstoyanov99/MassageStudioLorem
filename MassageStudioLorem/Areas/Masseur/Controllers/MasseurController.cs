namespace MassageStudioLorem.Areas.Masseur.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static MasseurConstants;

    [Area(MasseurAreaName)]
    [Authorize(Roles = MasseurRoleName)]
    public abstract class MasseurController : Controller
    {
    }
}
