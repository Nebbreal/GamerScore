using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.DTO
{
    public class Review
    {
        public int UserId { get; set; }//UserId gets assigned in the controller after token validation
        public int GameId { get; set; }
        public string? UserContext {  get; set; }
        public float StarRating { get; set; }
        public Review() { }
        public Review(int _gameId, string _userContext, float _starRating) 
        {
            GameId = _gameId;
            UserContext = _userContext;
            StarRating = _starRating;
        }

    }
}
