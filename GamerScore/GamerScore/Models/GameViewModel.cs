using GamerScore.DTO;
using System.Drawing;

namespace GamerScore.Models
{
    public class GameViewModel
    {
        public Game Game {  get; set; }
        public Review Review { get; set; }

        public GameViewModel() { }
        public GameViewModel(Game _game) 
        {
            Game = _game;
        }
    }
}
