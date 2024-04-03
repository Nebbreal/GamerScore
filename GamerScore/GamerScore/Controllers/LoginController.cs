using Microsoft.AspNetCore.Mvc;

namespace GamerScore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
