using Gamerscore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerscore.Core.Interfaces
{
    public interface IGenreDB
    {
        public bool CreateGenre(string _name, string? _imageUrl);
        public Genre GetGenreByName(string _name);
        public List<Genre> GetAllGenres();
    }
}
