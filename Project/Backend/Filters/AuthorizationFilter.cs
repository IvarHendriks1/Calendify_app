using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CalendifyApp.Filters{
    public class AuthorizationFilter : Attribute, IAsyncActionFilter {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext actionContext, ActionExecutionDelegate next)
        {
            var context = actionContext.HttpContext;
            string userSessionKey = "";
            if (context.Session.GetString("AdminLoggedIn") is not null){
                userSessionKey = "AdminLoggedIn";
            } else if (context.Session.GetString("UserLoggedIn") is not null){
                userSessionKey = "UserLoggedIn";
            }

            if (context.Session.GetString(userSessionKey) is null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            await next();
        }
    }
}