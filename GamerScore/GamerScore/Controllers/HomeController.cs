using Gamerscore.Core;
using Gamerscore.Core.Interfaces;
using GamerScore.DAL;
using GamerScore.DTO;
using GamerScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GamerScore.Controllers
{
    public class HomeController : Controller
    {
        private IGameRepository gameRepository;
        public HomeController(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public IActionResult Home()
        {
            //Get games
            List<Game> games = new List<Game>();
            try
            {
                GameManager gameManager = new(gameRepository);
                games = gameManager.GetAllGames();
            }
            catch (Exception ex)
            {
                //ToDo: throw exception
            }
            return View(new HomeViewModel(games));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
