using GamerScore.DTO;

namespace Gamerscore.Core.Interfaces.Services
{
    public interface IGameService
    {
        bool CreateGame(string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<string> _genreIds);
        List<Game> GetAllGames();
        Game GetGameById(int id);
    }
}