using System.Security.Claims;
using Commons.Identity.Services;

namespace Commons.Identity.Extensions
{
    public static partial class PrincipalExtensions
    {
        public static string GetFirstName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(UserClaims.FirstName);
        }
        public static string GetMiddleName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(UserClaims.MiddleName);
        }
        public static string GetLastName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(UserClaims.LastName);
        }
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Identity.Name;
        }
        public static string GetWorkSpaceId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(SettingsClaims.SelectedWorkSpaceId);
        }
        public static string GetWorkSpaceName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(SettingsClaims.SelectedWorkSpaceName);
        }
        public static string GetProfileTypeString(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(UserClaims.ProfileType);
        }
        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(c => c.Type == "IsAdmin");
        }
        public static bool IsWorkSpaceAdmin(this ClaimsPrincipal claimsPrincipal, string workSpaceId)
        {
            return claimsPrincipal.HasClaim(c => c.Type == "WorkSpaceAdmin" && c.Value == workSpaceId);
        }
    }
}
