using GamerScore.Options;
using GamerScore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GamerScore.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly JwtSettings _jwtSettings;
        public AdminPanelController(IOptions<JwtSettings> jwt)
        {
            this._jwtSettings = jwt.Value;
        }

        public IActionResult Panel()
        {
            //Validate if an admin is logged in
            bool isAdmin = false;
            var jwtToken = Request.Cookies["jwtToken"];
            if (jwtToken != null)
            {
                TokenService tokenService = new(_jwtSettings);
                isAdmin = tokenService.ValidateAdminLevelJwt(jwtToken);
            }
            
            if(isAdmin == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
            
        }

        public IActionResult AddGame()
        {
            return View();
        }

        public IActionResult AddGenre()
        {
            return View();
        }
    }
}
