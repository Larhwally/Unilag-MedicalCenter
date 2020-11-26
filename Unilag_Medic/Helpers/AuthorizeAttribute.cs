using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Unilag_Medic.ViewModel;
using System.Collections.Generic;

namespace Unilag_Medic.Helpers
{
    // Custom Authorization policy
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            UnilagMedLogin model = new UnilagMedLogin();
            // Add user details to httpContext to be passed to the next controller throught the middleware
            var user = (Dictionary<string, object>)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorize, Token expired" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}