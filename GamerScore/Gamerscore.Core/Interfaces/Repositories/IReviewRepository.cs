using GamerScore.DTO;

namespace Gamerscore.Core.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        public bool CreateReview(Review _review);
    }
}
