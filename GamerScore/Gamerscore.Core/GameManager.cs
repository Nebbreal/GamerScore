using Gamerscore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerscore.Core
{
    public class GameManager
    {
        IGameRepository gameRepository;
        public GameManager(IGameRepository _gameRepository) 
        {
            gameRepository = _gameRepository;
        }

        public bool CreateGame(string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<string> _genreIds)
        {
            List<int>parsedGenreIds = _genreIds.Select(int.Parse).ToList();
            return gameRepository.CreateGame(_title, _description, _developer, _thumbnailImageUrl, _imageUrls, parsedGenreIds);
        }
    }
}
