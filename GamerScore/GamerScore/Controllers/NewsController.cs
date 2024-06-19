using Microsoft.AspNetCore.Mvc;

namespace GamerScore.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult News()
        {
            return View();
        }
    }
}
