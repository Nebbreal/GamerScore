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
        public IActionResult EditGenre(EditGenreViewModel editGenreViewModel)
        {
            if(editGenreViewModel.GenreId == 0)
            {
                ModelState.AddModelError("GenreId", "You must select a genre.");
            }

            if(ModelState.IsValid)
            {
                int genreId = editGenreViewModel.GenreId;
                string genreName = editGenreViewModel.GenreName;
                string? genreImageUrl = editGenreViewModel.GenreImageUrl;
                if (genreService.EditGenre(genreId, genreName, genreImageUrl))
                {
                    TempData["SuccessMessage"] = $"Genre {genreName} successfully edited";
                }
                else
                {
                    ModelState.AddModelError("Error", "A unexpected error has occured");
                }
            }

            editGenreViewModel.AllGenres = genreService.GetAllGenres();

            return View("EditGenre", editGenreViewModel);
        }

        [HttpPost]
        public IActionResult DeleteGenre(EditGenreViewModel editGenreViewModel)
        {
            ModelState.Clear();

            if (editGenreViewModel.GenreId == 0)
            {
                ModelState.AddModelError("No genre selected", "Please select a genre");
            }

            if (genreService.DeleteGenre(editGenreViewModel.GenreId))
            {
                TempData["SuccessMessage"] = "Genre successfully removed";
            }
            else
            {
                ModelState.AddModelError("Error", "A unexpected error has occured");
            }

            editGenreViewModel.AllGenres = genreService.GetAllGenres();

            return View("EditGenre", editGenreViewModel);
        }
    }
}
