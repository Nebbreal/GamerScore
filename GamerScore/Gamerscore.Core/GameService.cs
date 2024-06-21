using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.Core.Interfaces.Services;
using GamerScore.Domain;

namespace Gamerscore.Core
{
    public class GameService : IGameService
    {
        private IGameRepository gameRepository;
        public GameService(IGameRepository _gameRepository)
        {
            gameRepository = _gameRepository;
        }

        public List<Game> GetAllGames()
        {
            List<Game> games = gameRepository.GetAllGames();
            return games;
        }

        public Game GetGameById(int id)
        {
            try
            {
                Game game = gameRepository.GetGameById(id);
                return game;
            }
            catch (Exception e)
            {
                MessageLogger.Log($"Exception occured: {e}");
                Game game = new(id, "Failed fetching game", "Failed fetching game", "Failed fetching game", "Failed fetching game");
                return game;
            }
        }

        public List<Game> GetGamesBySearchQuery(string _searchQuery)
        {
            return gameRepository.GetGamesBySearchQuery(_searchQuery);
        }

        public bool CreateGame(string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<string> _genreIds)
        {
            List<int> parsedGenreIds = _genreIds.Select(int.Parse).ToList();
            return gameRepository.CreateGame(_title, _description, _developer, _thumbnailImageUrl, _imageUrls, parsedGenreIds);
        }

        public bool EditGame(int _gameId, string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<string> _genreIds)
        {
            List<int> parsedGenreIds = _genreIds.Select(int.Parse).ToList();
            return gameRepository.EditGame(_gameId ,_title, _description, _developer, _thumbnailImageUrl, _imageUrls, parsedGenreIds);
        }
        
        public bool DeleteGame(int _gameId)
        {
            return gameRepository.DeleteGame(_gameId);
        }
    }
}
