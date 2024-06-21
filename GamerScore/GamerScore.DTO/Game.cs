namespace GamerScore.Domain
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string ThumbnailImageUrl { get; set; }

        public List<GameImage>? ImageUrls { get; set; }

        public Game() { }

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
