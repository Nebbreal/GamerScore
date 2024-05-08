using Gamerscore.Core.Interfaces;
using Gamerscore.Core.Models;

namespace Gamerscore.Core
{
    public class GenreManager
    {
        public List<Genre> GetAllGenres(IGenreDB _genreDB)
        {
            List<Genre> genres = _genreDB.GetAllGenres();

            return genres;
        }

        public bool CreateGenre(IGenreDB _genreDB, string _name, string? _imageUrl) 
        {
            Genre genreInDatabase = _genreDB.GetGenreByName(_name);
            if(_name == genreInDatabase.Name)
            {
                return false;
            }
            else
            {
                return _genreDB.CreateGenre(_name, _imageUrl);
            }

        }
    }
}
