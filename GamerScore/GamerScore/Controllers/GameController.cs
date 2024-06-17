using Gamerscore.Core.Interfaces.Services;
using GamerScore.Attributes;
using GamerScore.DTO;
using GamerScore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
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

        public IActionResult Game(int gameId)
        {
            int userId = GetUserIdFromJwtOrDefault();

            GameViewModel gameViewModel = new GameViewModel()
            {
                Game = gameService.GetGameById(gameId),
                Review = reviewService.GetReviewByGameAndUserIdOrDefault(gameId, userId),
                AllReviews = reviewService.GetAllReviewsByGameId(gameId)
            };
            
            return View("Game", gameViewModel);
        }

        [HttpPost, LoginRequired]
        public IActionResult PostReview(GameViewModel gameViewModel)
        {
            //Model validation
            Review review = gameViewModel.Review;
            if (review.StarRating < 1) { review.StarRating = 1; }
            
            //Get accountId
            int userId = GetUserIdFromJwtOrDefault();

            if (userId < 1)
            {
                gameViewModel.ErrorMessage = "Error creating review"; //ToDo: implement this somehow
                return RedirectToAction("Game", new { gameId = review.GameId });
            }

            review.UserId = userId;

            reviewService.CreateReview(review);
            gameViewModel.SuccessMessage = "Review successfully created!";

            return RedirectToAction("Game", new { gameId = review.GameId });
        }

        [HttpPost, LoginRequired]
        public IActionResult DeleteReview (int gameId)
        {
            int userId = GetUserIdFromJwtOrDefault();

            if (userId < 1)
            {
                //gameViewModel.ErrorMessage = "Error removing review";
                return RedirectToAction("Game", new { gameId = gameId });
            }

            reviewService.DeleteReviewByGameIdAndUserId(gameId, userId);

            return RedirectToAction("Game", new { gameId = gameId });
        }

        private int GetUserIdFromJwtOrDefault()
        {
            var jwtToken = Request.Cookies["jwtToken"];
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken? jwtSecurityToken = handler.CanReadToken(jwtToken) ? handler.ReadJwtToken(jwtToken) : null;

            int userId = -1;
            if (jwtSecurityToken != null)
            {
                userId = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "AccountId").Value); //ToDo: betere manier hiervoor?
            }

            return userId;
        }
    }
}
