using Gamerscore.Core;
using Gamerscore.Core.Interfaces;
using Gamerscore.DTO;
using GamerScore.Models;
using GamerScore.Options;
using GamerScore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GamerScore.Controllers
{
    public class AdminPanelController : Controller
    {
        private IGameRepository gameRepository;
        private IGenreRepository genreRepository;
        private readonly JwtSettings jwtSettings;
        public AdminPanelController(IGameRepository gameRepository, IGenreRepository genreRepository, IOptions<JwtSettings> jwt)
        {
            this.gameRepository = gameRepository;
            this.genreRepository = genreRepository;
            this.jwtSettings = jwt.Value;
        }

        public IActionResult Panel()
        {
            //Validate if an admin is logged in
            bool isAdmin = false;
            var jwtToken = Request.Cookies["jwtToken"];
            if (jwtToken != null)
            {
                TokenService tokenService = new(jwtSettings);
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
            GenreManager genreManager = new(genreRepository);

            List<Genre> genres = genreManager.GetAllGenres();
            
            AddGameViewModel model = new(genres);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddGame(AddGameViewModel _AddGameViewModel)
        {
            GameManager gameManager = new GameManager(gameRepository);

            gameManager.CreateGame(_AddGameViewModel.Name, _AddGameViewModel.Description, _AddGameViewModel.Developer, _AddGameViewModel.ThumbnailImageUrl, _AddGameViewModel.ImageUrl, _AddGameViewModel.SelectedGenres);
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

            GenreManager genreManager = new GenreManager(genreRepository);

            if(genreManager.CreateGenre(_addGenreViewModel.Name, _addGenreViewModel.ImageUrl))
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
