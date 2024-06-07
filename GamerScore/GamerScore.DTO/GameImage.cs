using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.DTO
{
    public class GameImage
    {
        public string Name { get; private set; } = string.Empty;
        public string ImageUrl { get; private set; } = string.Empty;

        public GameImage(string _name, string _ImageUrl) 
        {
            Name = _name;
            ImageUrl = _ImageUrl;
        }
    }
}
