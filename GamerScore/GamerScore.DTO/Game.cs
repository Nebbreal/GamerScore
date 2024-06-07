using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerScore.DTO
{
    public class Game
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Developer { get; private set; }
        public string ThumbnailImageUrl { get; private set; }

        public List<GameImage>? ImageUrls { get; private set; }

        public Game(int _id, string _title, string _description, string _developer, string _thumbnailImageUrl)
        {
            Id = _id;
            Title = _title;
            Description = _description;
            Developer = _developer;
            ThumbnailImageUrl = _thumbnailImageUrl;
        }

        public Game(int _id, string _title, string _description, string _developer, string _thumbnailImageUrl, List<GameImage>? _images)
        {
            Id = _id;
            Title = _title;
            Description = _description;
            Developer = _developer;
            ThumbnailImageUrl = _thumbnailImageUrl;
            ImageUrls = _images;
        }
    }
}
