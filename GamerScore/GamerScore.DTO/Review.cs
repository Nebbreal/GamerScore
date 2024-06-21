using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.Domain
{
    public class Review
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int GameId { get; set; }
        public string? UserContext {  get; set; }
        public float StarRating { get; set; }
        public DateTime createdAt { get; set; }
        public Review() { }

    }
}
