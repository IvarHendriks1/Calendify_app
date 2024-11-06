using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CalendifyApp.Filters{
    public class AdminFilter : Attribute, IAsyncActionFilter {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext actionContext, ActionExecutionDelegate next)
        {
            var context = actionContext.HttpContext;

            if (context.Session.GetString("AdminLoggedIn") is null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            await next();
        }
    }
}