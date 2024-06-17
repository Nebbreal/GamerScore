using GamerScore.DTO;

namespace Gamerscore.Core.Interfaces.Services
{
    public interface IReviewService
    {
        bool CreateReview(Review _review);
        Review GetReviewByGameAndUserIdOrDefault(int _gameId, int _userId);

    }
}