using Gamerscore.DTO;

namespace Gamerscore.Core.Interfaces.Services
{
    public interface IGenreService
    {
        List<Genre> GetAllGenres();
        bool CreateGenre(string _name, string? _imageUrl);
        public bool EditGenre(int _genreId, string _name, string? _imageUrl);
        public bool DeleteGenre(int _genreId);
    }
}