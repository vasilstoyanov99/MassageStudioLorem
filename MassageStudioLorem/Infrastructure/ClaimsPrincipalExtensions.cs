namespace MassageStudioLorem.Infrastructure
{
    using System.Security.Claims;
    using static Areas.Client.ClientConstants;
    using static Areas.Masseur.MasseurConstants;
    using static Areas.Admin.AdminConstants;
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsClient(this ClaimsPrincipal user)
            => user.IsInRole(ClientRoleName);

        public static bool IsMasseur(this ClaimsPrincipal user)
            => user.IsInRole(MasseurRoleName);

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdminRoleName);
    }
}
