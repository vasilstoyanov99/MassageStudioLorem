namespace MassageStudioLorem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static AdminConstants;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]

    public class AdminController : Controller
    {
    }
}
