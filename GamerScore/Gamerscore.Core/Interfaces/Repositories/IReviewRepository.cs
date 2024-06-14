namespace Gamerscore.Core.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        public bool CreateReview(int userId, int gameId, string userContext, int starRating);
    }
}
