using GamerScore.DTO;

namespace GamerScore.Models
{
    public class HomeViewModel
    {
        public List<Game> games { get; private set; }

        public HomeViewModel(List<Game> games)
        {
            this.games = games;
        }
    }
}
