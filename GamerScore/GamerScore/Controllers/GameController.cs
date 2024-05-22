using Gamerscore.Core;
using Gamerscore.Core.Interfaces;
using GamerScore.DTO;
using GamerScore.Models;
using GamerScore.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GamerScore.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository gameRepository;
        public GameController(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public IActionResult Game(int gameId)
        {
            GameManager gameManager = new GameManager(gameRepository);
            GameViewModel game = new(gameManager.GetGameById(gameId)); //ToDo: this fetches only the first image
            
            return View(game);
        }
    }
}
