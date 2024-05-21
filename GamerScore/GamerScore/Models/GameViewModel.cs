using GamerScore.DTO;
using System.Drawing;

namespace GamerScore.Models
{
    public class GameViewModel
    {
        public Game game {  get; private set; }

        public GameViewModel(Game _game) 
        {
            game = _game;
        }
    }
}
