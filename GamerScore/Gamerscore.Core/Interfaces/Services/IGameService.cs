using GamerScore.DTO;

namespace Gamerscore.Core.Interfaces.Services
{
    public interface IGameService
    {
        List<Game> GetAllGames();
        Game GetGameById(int id);
        public List<Game> GetGamesBySearchQuery(string _searchQuery);
        bool CreateGame(string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<string> _genreIds);
        public bool EditGame(int _gameId, string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<string> _genreIds);
        public bool DeleteGame(int _gameId);
    }
}