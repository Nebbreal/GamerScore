using Gamerscore.Core.Interfaces.Services;
using GamerScore.DTO;
using GamerScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GamerScore.Controllers
{
    public class HomeController : Controller
    {
        private IGameService gameService;
        public HomeController(IGameService _gameService)
        {
            gameService = _gameService;
        }

        public IActionResult Home()
        {
            //Get games
            List<Game> games = new List<Game>();
            try
            {
                games = gameService.GetAllGames();
            }
            catch (Exception ex)
            {
                //ToDo: throw exception
            }
            return View(new HomeViewModel(games));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() //ToDo: make error view
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
