using Gamerscore.DTO;

namespace Gamerscore.Core.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        public List<Genre> GetAllGenres();
        public Genre GetGenreByName(string _name);
        public bool CreateGenre(string _name, string? _imageUrl);
        public bool EditGenre(int _genreId, string _name, string? _imageUrl);
        public bool DeleteGenre(int _genreId);
    }
}
