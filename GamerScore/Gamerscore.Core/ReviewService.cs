using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.Core.Interfaces.Services;
using GamerScore.DTO;

namespace Gamerscore.Core
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository reviewRepository;
        public ReviewService(IReviewRepository _reviewRepository)
        {
            reviewRepository = _reviewRepository;
        }

        public bool CreateReview(Review _review)
        {
            return reviewRepository.CreateReview(_review);
        }

        public Review GetReviewByGameAndUserIdOrDefault(int _gameId, int _userId)
        {
            if (_userId < 1)
            {
                return new Review();
            }

            return reviewRepository.GetReviewByGameAndUserIdOrDefault(_gameId, _userId);
        }

    }
}
