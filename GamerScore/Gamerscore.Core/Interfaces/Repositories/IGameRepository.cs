﻿using GamerScore.Domain;

namespace Gamerscore.Core.Interfaces.Repositories
{
    public interface IGameRepository
    {
        public List<Game> GetAllGames();
        public Game GetGameById(int _gameId);
        public List<Game> GetGamesBySearchQuery(string _searchQuery);
        public bool CreateGame(string _title, string description, string developer, string _thumbnailImageUrl, List<string> _imageUrls, List<int> _genreIds);
        public bool EditGame(int _gameId, string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<int> _genreIds);
        public bool DeleteGame(int _gameId);
    }
}
