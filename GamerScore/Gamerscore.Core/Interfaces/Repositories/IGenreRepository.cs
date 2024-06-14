using Gamerscore.DTO;

namespace Gamerscore.Core.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        public bool CreateGenre(string _name, string? _imageUrl);
        public Genre GetGenreByName(string _name);
        public List<Genre> GetAllGenres();
    }
}
