using GamerScore.DTO;

namespace Gamerscore.Core.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        public bool CreateReview(Review _review);
        public Review GetReviewByGameAndUserIdOrDefault(int _gameId, int _userId);
    }
}
