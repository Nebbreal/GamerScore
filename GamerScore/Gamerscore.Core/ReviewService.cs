using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.Core.Interfaces.Services;
using GamerScore.Domain;

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

        public bool DeleteReviewByGameIdAndUserId(int _gameId, int _userId)
        {
            return reviewRepository.DeleteReviewByGameIdAndUserId(_gameId, _userId);
        }

        public List<Review> GetAllReviewsByGameId(int _gameId) 
        {
            return reviewRepository.GetAllReviewsByGameIdOrDefault(_gameId);
        }

    }
}
