﻿using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace GigHub.IntegrationTests.Extensions
{
    public static class ControllerExtensions
    {
        private const string ClaimTypeNameUri =
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        private const string ClaimTypeNameIdentifierUri =
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public static void MockCurrentUser(this Controller controller, string userId, string userName)
        {
            var securityClaims = new List<Claim> {
                new Claim(ClaimTypeNameIdentifierUri, userId),
                new Claim(ClaimTypeNameUri, userName)
            };

            var identity = new GenericIdentity("");
            identity.AddClaims(securityClaims);
            var principal = new GenericPrincipal(identity, null);
            
            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext == Mock.Of<HttpContextBase>(http =>
                    http.User == principal));
        }
    }
}