using Microsoft.AspNetCore.Mvc;

namespace GamerScore.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
