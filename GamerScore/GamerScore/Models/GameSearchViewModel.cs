using GamerScore.DTO;

namespace GamerScore.Models
{
    public class GameSearchViewModel
    {
        public List<Game> QueriedGames { get; set; }
        public string SearchQuery { get; set; }

        public GameSearchViewModel() { }
    }
}
