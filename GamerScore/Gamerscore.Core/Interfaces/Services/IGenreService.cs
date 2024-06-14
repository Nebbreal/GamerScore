using Gamerscore.DTO;

namespace Gamerscore.Core.Interfaces.Services
{
    public interface IGenreService
    {
        bool CreateGenre(string _name, string? _imageUrl);
        List<Genre> GetAllGenres();
    }
}