using Microsoft.AspNetCore.Mvc;

namespace GamerScore.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Overview()
        {
            return View();
        }
    }
}
