using Gamerscore.Core.Interfaces;
using Gamerscore.DTO;

namespace Gamerscore.Core
{
    public class GenreManager
    {
        IGenreRepository genreRepository;
        public GenreManager(IGenreRepository _genreRepository) 
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
            if(_name == genreInDatabase.Name)
            {
                return false;
            }
            else
            {
                return genreRepository.CreateGenre(_name, _imageUrl);
            }

        }
    }
}
