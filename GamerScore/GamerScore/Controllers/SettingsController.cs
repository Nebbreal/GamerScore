using Microsoft.AspNetCore.Mvc;

namespace GamerScore.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult UserSettings()
        {
            return View();
        }
    }
}
