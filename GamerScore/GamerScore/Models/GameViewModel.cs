using GamerScore.Domain;
using System.Drawing;

namespace GamerScore.Models
{
    public class GameViewModel
    {
        public Game Game {  get; set; }
        public Review Review { get; set; }
        public List<Review> AllReviews { get; set; }
        public string? ErrorMessage {  get; set; }
        public string? SuccessMessage { get; set; }

        public GameViewModel() { }
    }
}
