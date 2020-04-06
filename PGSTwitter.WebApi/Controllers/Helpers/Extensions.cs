namespace PGSTwitter.WebApi.Controllers.Helpers
{
    using System.Linq;
    using System.Security.Claims;

    public static class Extensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            var id = user.Claims
                .Single(c => c.Type == ClaimTypes.NameIdentifier)
                .Value;

            return id;
        }
    }
}
