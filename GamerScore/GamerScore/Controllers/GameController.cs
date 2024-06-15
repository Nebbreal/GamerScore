using Gamerscore.Core.Interfaces.Services;
using GamerScore.Attributes;
using GamerScore.DTO;
using GamerScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace GamerScore.Controllers
{
    public class GameController : Controller
    {
        private IGameService gameService;
        private IReviewService reviewService;

        public GameController(IGameService _gameService, IReviewService _reviewService)
        {
            gameService = _gameService;
            reviewService = _reviewService;
        }

        public IActionResult Game(int gameId, GameViewModel? gameViewModel)
        {
            if (gameViewModel != null && gameViewModel.Game != null)
            {
                return View(gameViewModel);
            }
            GameViewModel game = new()
            {
                Game = gameService.GetGameById(gameId),
                Review = new Review()
            };

            return View(game);
        }

        [HttpPost, LoginRequired]
        public IActionResult PostReview(GameViewModel gameViewModel)
        {
            //Model validation
            Review review = gameViewModel.Review;
            if (review.StarRating < 1) { review.StarRating = 1; }
            //Get accountId
            var jwtToken = Request.Cookies["jwtToken"];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwtToken);

            int userId;
            if (int.TryParse(jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "AccountId").Value, out userId))
            {
                review.UserId = userId;
            }
            else
            {
                gameViewModel.ErrorMessage = "Error creating review";
                return RedirectToAction("Game", new { gameId = review.GameId, gameViewModel = gameViewModel });
            }

            reviewService.CreateReview(review);
            gameViewModel.SuccessMessage = "Review successfully created!";
            return RedirectToAction("Game", new { gameId = review.GameId, gameViewModel = gameViewModel });
        }
    }
}
