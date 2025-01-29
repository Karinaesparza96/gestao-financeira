using Business.Interfaces;
using System.Security.Claims;

namespace Api.Extensions
{
    public class AppIdentityUser(IHttpContextAccessor httpContextAccessor) : IAppIdentityUser
    {
        public bool IsAutenticated()
        {
            return httpContextAccessor.HttpContext?.User.Identity is { IsAuthenticated: true };
        }

        public bool IsOwner(string? idIdentityUser)
        {   
            if (string.IsNullOrEmpty(idIdentityUser)) return false;

            return idIdentityUser == GetUserId();
        }

        public string GetUserId()
        {
            if (!IsAutenticated()) return string.Empty;

            var claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return claim ?? string.Empty;
        }
    }
}
