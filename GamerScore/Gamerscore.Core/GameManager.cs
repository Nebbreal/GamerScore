using Gamerscore.Core.Interfaces;
using GamerScore.DTO;

namespace Gamerscore.Core
{
    public class GameManager
    {
        private IGameRepository gameRepository;
        public GameManager(IGameRepository _gameRepository) 
        {
            gameRepository = _gameRepository;
        }

        public bool CreateGame(string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<string> _genreIds)
        {
            List<int>parsedGenreIds = _genreIds.Select(int.Parse).ToList();
            return gameRepository.CreateGame(_title, _description, _developer, _thumbnailImageUrl, _imageUrls, parsedGenreIds);
        }

        public List<Game> GetAllGames()
        {
            List<Game> games = gameRepository.GetAllGames();
            return games;
        }

        //ToDo: finish this
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
    }
}
