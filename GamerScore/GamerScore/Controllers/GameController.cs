using Microsoft.AspNetCore.Mvc;

namespace GamerScore.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Game()
        {
            return View();
        }
    }
}
