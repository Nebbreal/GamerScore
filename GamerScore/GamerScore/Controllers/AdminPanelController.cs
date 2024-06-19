using Gamerscore.Core.Interfaces.Services;
using Gamerscore.DTO;
using GamerScore.Attributes;
using GamerScore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerScore.Controllers
{
    [AdminRequired]
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
            return View();
        }

        public IActionResult AddGame()
        {
            List<Genre> genres = genreService.GetAllGenres();
            
            AddGameViewModel addGameviewModel = new(genres);
            return View(addGameviewModel);
        }
        [HttpPost]
        public IActionResult AddGame(AddGameViewModel _AddGameViewModel)
        {
            gameService.CreateGame(_AddGameViewModel.Name, _AddGameViewModel.Description, _AddGameViewModel.Developer, _AddGameViewModel.ThumbnailImageUrl, _AddGameViewModel.ImageUrl, _AddGameViewModel.SelectedGenres);
            return RedirectToAction("Panel");
        }

        public IActionResult EditGame()
        {
            EditGameViewModel editGameViewModel = new EditGameViewModel();
            editGameViewModel.AllGames = gameService.GetAllGames();
            editGameViewModel.Genres = genreService.GetAllGenres();

            return View(editGameViewModel);
        }

        [HttpPost]
        public IActionResult EditGame(EditGameViewModel _editGameViewModel)
        {
            int gameId = _editGameViewModel.GameId;
            string title = _editGameViewModel.Name;
            string description = _editGameViewModel.Description;
            string developer = _editGameViewModel.Developer;
            string thumbnailImageUrl = _editGameViewModel.ThumbnailImageUrl;
            List<string>? imageUrls = _editGameViewModel.ImageUrl;
            List<string>? selectedGenres = _editGameViewModel.SelectedGenres;

            if (ModelState.IsValid)
            {
                if (gameService.EditGame(gameId, title, description, developer, thumbnailImageUrl, imageUrls, selectedGenres))
                {
                    TempData["SuccessMessage"] = "Game successfully edited";
                }
                else
                {
                    ModelState.AddModelError("Error", "An unexpected error has occured");
                }
            }
            
            _editGameViewModel.AllGames = gameService.GetAllGames();
            _editGameViewModel.Genres = genreService.GetAllGenres();
            return View(_editGameViewModel);
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

        public IActionResult EditGenre()
        {
            EditGenreViewModel editGenreViewModel = new EditGenreViewModel();
            List<Genre> genres = genreService.GetAllGenres();

            editGenreViewModel.AllGenres = genres;

            return View(editGenreViewModel);
        }

        [HttpPost]
        public IActionResult EditGenre(EditGenreViewModel _editGenreViewModel)
        {
            if(_editGenreViewModel.GenreId == 0)
            {
                ModelState.AddModelError("GenreId", "You must select a genre.");
            }

            if(ModelState.IsValid)
            {
                int genreId = _editGenreViewModel.GenreId;
                string genreName = _editGenreViewModel.GenreName;
                string? genreImageUrl = _editGenreViewModel.GenreImageUrl;
                if (genreService.EditGenre(genreId, genreName, genreImageUrl))
                {
                    TempData["SuccessMessage"] = $"Genre {genreName} successfully edited";
                }
                else
                {
                    ModelState.AddModelError("Error", "A unexpected error has occured");
                }
            }

            _editGenreViewModel.AllGenres = genreService.GetAllGenres();

            return View("EditGenre", _editGenreViewModel);
        }

        [HttpPost]
        public IActionResult DeleteGenre(EditGenreViewModel _editGenreViewModel)
        {
            ModelState.Clear();

            if (_editGenreViewModel.GenreId == 0)
            {
                ModelState.AddModelError("No genre selected", "Please select a genre");
            }

            if (genreService.DeleteGenre(_editGenreViewModel.GenreId))
            {
                TempData["SuccessMessage"] = "Genre successfully removed";
            }
            else
            {
                ModelState.AddModelError("Error", "A unexpected error has occured");
            }

            _editGenreViewModel.AllGenres = genreService.GetAllGenres();

            return View("EditGenre", _editGenreViewModel);
        }
    }
}
