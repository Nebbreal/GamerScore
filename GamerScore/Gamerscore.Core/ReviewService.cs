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
    }
}
