using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerscore.Core.Interfaces
{
    public interface IGameDB
    {
        public bool CreateGame(string _title, string description, string developer, string _thumbnailImageUrl, List<string> _imageUrls, List<int> _genreIds);
    }
}
