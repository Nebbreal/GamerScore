using Gamerscore.Core.Interfaces.Services;
using GamerScore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerScore.Controllers
{
    public class SearchController : Controller
    {
        private IGameService gameService;
        public SearchController(IGameService _gameService) 
        {
            gameService = _gameService;
        }

        public IActionResult GameSearch(string searchQuery)
        {
            GameSearchViewModel gameSearchViewModel = new GameSearchViewModel();
            gameSearchViewModel.SearchQuery = searchQuery;
            gameSearchViewModel.QueriedGames = gameService.GetGamesBySearchQuery(searchQuery);

            return View(gameSearchViewModel);
        }
    }
}
