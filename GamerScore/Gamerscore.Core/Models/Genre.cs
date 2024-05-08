namespace Gamerscore.Core.Models
{
    public class Genre
    {
        public int? Id { get; private set; }
        public string? Name { get; private set; }
        public string? ImageUrl { get; private set; }

        public Genre(int? _id, string? _name, string? _imageUrl) 
        {
            this.Id = _id;
            this.Name = _name;
            this.ImageUrl = _imageUrl;
        }
    }
}
