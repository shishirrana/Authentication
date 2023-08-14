using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace reviseAuthentication.Filter
{
    public class MyCustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string controllerName = context.HttpContext.Request.RouteValues["controller"].ToString();
            string actionName = context.HttpContext.Request.RouteValues["action"].ToString();

           /* if (actionName.ToLower() != "accessdenied")

                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"Area", "" },
                    {"controller", "home" },
                    {"action", "AccessDenied" }
                });*/


            return;
        }
    }
}
