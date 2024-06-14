using Gamerscore.Core.Interfaces.Services;
using Gamerscore.DTO;
using GamerScore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerScore.Controllers
{
    public class AdminPanelController : Controller
    {
        private IGameService gameService;
        private IGenreService genreService;
        private ITokenService tokenService;
        public AdminPanelController(IGameService _gameService, IGenreService _genreService, ITokenService _tokenService)
        {
            gameService = _gameService;
            genreService = _genreService;
            tokenService = _tokenService;
        }

        public IActionResult Panel()
        {
            //Validate if an admin is logged in
            bool isAdmin = false;
            var jwtToken = Request.Cookies["jwtToken"];
            if (jwtToken != null)
            {
                isAdmin = tokenService.ValidateAdminLevelJwt(jwtToken);
            }
            
            if(isAdmin == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
            
        }

        public IActionResult AddGame()
        {
            List<Genre> genres = genreService.GetAllGenres();
            
            AddGameViewModel model = new(genres);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddGame(AddGameViewModel _AddGameViewModel)
        {
            gameService.CreateGame(_AddGameViewModel.Name, _AddGameViewModel.Description, _AddGameViewModel.Developer, _AddGameViewModel.ThumbnailImageUrl, _AddGameViewModel.ImageUrl, _AddGameViewModel.SelectedGenres);
            return RedirectToAction("Panel");
        }

        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(AddGenreViewModel _addGenreViewModel) 
        { 
            if(_addGenreViewModel.Name == null)
            {
                _addGenreViewModel.ErrorMessage = "A name is required";
                return View(_addGenreViewModel);
            }

            if(genreService.CreateGenre(_addGenreViewModel.Name, _addGenreViewModel.ImageUrl))
            {
                AddGenreViewModel successModel = new AddGenreViewModel();
                successModel.SuccessMessage = "Genre creation success!";
                return View(successModel);
            }
            else 
            {
                _addGenreViewModel.ErrorMessage = "Name already exists or something went wrong";
                return View(_addGenreViewModel);
            }
        }
    }
}
