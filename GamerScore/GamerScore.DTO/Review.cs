using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.DTO
{
    public class Review
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int GameId { get; set; }
        public string? UserContext {  get; set; }
        public float StarRating { get; set; }
        public DateTime createdAt { get; set; }
        public Review() { }

    }
}
