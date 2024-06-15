using Gamerscore.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GamerScore.Attributes
{
    public class LoginRequiredAttribute : ActionFilterAttribute
    {
        public LoginRequiredAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ITokenService tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();

            if (context.HttpContext.Request.Cookies.TryGetValue("jwtToken", out string? jwtToken))
            {
                if (tokenService.ValidateUserLevelJwt(jwtToken))
                {
                    return;
                }
            }

            context.HttpContext.Response.Cookies.Delete("jwtToken");
            context.Result = new RedirectToActionResult("Login", "Login", null);
        }
    }
}
