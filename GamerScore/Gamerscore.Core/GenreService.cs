using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.Core.Interfaces.Services;
using Gamerscore.DTO;

namespace Gamerscore.Core
{
    public class GenreService : IGenreService
    {
        IGenreRepository genreRepository;
        public GenreService(IGenreRepository _genreRepository)
        {
            genreRepository = _genreRepository;
        }

        public List<Genre> GetAllGenres()
        {
            List<Genre> genres = genreRepository.GetAllGenres();

            return genres;
        }

        public bool CreateGenre(string _name, string? _imageUrl)
        {
            Genre genreInDatabase = genreRepository.GetGenreByName(_name);
            if (_name == genreInDatabase.Name)
            {
                return false;
            }
            else
            {
                return genreRepository.CreateGenre(_name, _imageUrl);
            }
        }

        public bool EditGenre(int _genreId, string _name, string? _imageUrl)
        {
            return genreRepository.EditGenre(_genreId, _name, _imageUrl);
        }

        public bool DeleteGenre(int _genreId)
        {
            return genreRepository.DeleteGenre(_genreId);
        }
    }
}
