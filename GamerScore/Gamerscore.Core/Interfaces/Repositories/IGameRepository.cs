using GamerScore.DTO;

namespace Gamerscore.Core.Interfaces.Repositories
{
    public interface IGameRepository
    {
        public bool CreateGame(string _title, string description, string developer, string _thumbnailImageUrl, List<string> _imageUrls, List<int> _genreIds);
        public List<Game> GetAllGames();
        public Game GetGameById(int _gameId);
    }
}
