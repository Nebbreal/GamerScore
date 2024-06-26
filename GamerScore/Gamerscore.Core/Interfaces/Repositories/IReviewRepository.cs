﻿using GamerScore.Domain;

namespace Gamerscore.Core.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        public bool CreateReview(Review _review);
        public Review GetReviewByGameAndUserIdOrDefault(int _gameId, int _userId);
        public bool DeleteReviewByGameIdAndUserId(int _gameId, int _userId);
        public List<Review> GetAllReviewsByGameIdOrDefault(int _gameId);

    }
}
