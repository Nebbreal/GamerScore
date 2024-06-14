using Gamerscore.Core;
using Gamerscore.Core.Interfaces;
using Gamerscore.Core.Interfaces.Services;
using GamerScore.DTO;
using GamerScore.Models;
using GamerScore.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GamerScore.Controllers
{
    public class GameController : Controller
    {
        private IGameService gameService;
        public GameController(IGameService _gameService)
        {
            gameService = _gameService;
        }

        public IActionResult Game(int gameId)
        {
            GameViewModel game = new(gameService.GetGameById(gameId));
            game.Review = new Review();
            return View(game);
        }

        [HttpPost]
        public IActionResult PostReview(GameViewModel gameViewModel)
        {
            //return RedirectToAction("Game", new RouteValueDictionary(
            //    new { action = "Game", gameId = gameViewModel.Game.Id }));
            return RedirectToAction("Home", "Home");
        }
    }
}
