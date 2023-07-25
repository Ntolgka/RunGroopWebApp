using System.Security.Claims;

namespace RunGroopWebApp
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return null;
            }
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
