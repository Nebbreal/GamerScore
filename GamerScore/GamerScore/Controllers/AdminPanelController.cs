using Gamerscore.Core;
using Gamerscore.Core.Models;
using GamerScore.DAL;
using GamerScore.Models;
using GamerScore.Options;
using GamerScore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GamerScore.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly JwtSettings _jwtSettings;
        public AdminPanelController(IOptions<ConnectionStrings> connectionStrings, IOptions<JwtSettings> jwt)
        {
            this._connectionStrings = connectionStrings.Value;
            this._jwtSettings = jwt.Value;
        }

        public IActionResult Panel()
        {
            //Validate if an admin is logged in
            bool isAdmin = false;
            var jwtToken = Request.Cookies["jwtToken"];
            if (jwtToken != null)
            {
                TokenService tokenService = new(_jwtSettings);
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
            GenreManager genreManager = new();
            GenreDB genreDB = new(_connectionStrings.DBConnectionString);
            List<Genre> genres = genreManager.GetAllGenres(genreDB);
            
            AddGameViewModel model = new(genres);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddGame(AddGameViewModel _model)
        {
            GameDB gameDB = new(_connectionStrings.DBConnectionString);
            GameManager gameManager = new GameManager();

            gameManager.CreateGame(gameDB, _model.Name, _model.Description, _model.Developer, _model.ThumbnailImageUrl, _model.ImageUrl, _model.SelectedGenres);
            return RedirectToAction("Panel");
        }

        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddGenre(AddGenreViewModel _model) 
        { 
            if(_model.Name == null)
            {
                _model.ErrorMessage = "A name is required";
                return View(_model);
            }

            GenreDB genreDB = new(_connectionStrings.DBConnectionString);
            GenreManager genreManager = new GenreManager();

            if(genreManager.CreateGenre(genreDB, _model.Name, _model.ImageUrl))
            {
                AddGenreViewModel successModel = new AddGenreViewModel();
                successModel.SuccessMessage = "Genre creation success!";
                return View(successModel);
            }
            else 
            {
                _model.ErrorMessage = "Name already exists or something went wrong";
                return View(_model);
            }
        }
    }
}
