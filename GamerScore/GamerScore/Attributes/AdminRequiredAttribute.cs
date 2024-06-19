using Gamerscore.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GamerScore.Attributes
{
    public class AdminRequiredAttribute : ActionFilterAttribute
    {
        public AdminRequiredAttribute() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ITokenService tokenService = context.HttpContext.RequestServices.GetService<ITokenService>() ?? throw new InvalidOperationException("ITokenService not found in AdminRequiredAttribute");

            if (context.HttpContext.Request.Cookies.TryGetValue("jwtToken", out string? jwtToken))
            {
                if (tokenService.ValidateAdminLevelJwt(jwtToken))
                {
                    return;
                }
            }

            context.Result = new RedirectToActionResult("Home", "Home", null);
        }
    }
}
