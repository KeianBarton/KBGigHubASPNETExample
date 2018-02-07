using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace GigHub.Tests.Extensions
{
    public static class ApiControllerExtensions
    {
        private const string ClaimTypeNameUri =
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        private const string ClaimTypeNameIdentifierUri =
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public static void MockCurrentUser(this ApiController controller, string userId, string userName)
        {
            var securityClaims = new List<Claim> {
                new Claim(ClaimTypeNameIdentifierUri, userId),
                new Claim(ClaimTypeNameUri, userName)
            };

            var identity = new GenericIdentity("");
            identity.AddClaims(securityClaims);
            var principal = new GenericPrincipal(identity, null);

            controller.User = principal;
        }
    }
}
