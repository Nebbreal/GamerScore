using Gamerscore.DTO;

namespace GamerScore.Models
{
    public class AddGameViewModel
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public string? Developer { get; private set; }
        public string ThumbnailImageUrl { get; private set; }
        public List<string>? ImageUrl { get; private set; }
        public List<string>? SelectedGenres {  get; private set; }
        public List<Genre>? Genres { get; private set; }


        public AddGameViewModel() { }
        public AddGameViewModel(string _name, string _description, string _developer, string _thumbnailImageUrl , List<string> _ImageUrl, List<string> _selectedGenres)
        {
            Name = _name;
            Description = _description;
            ImageUrl = _ImageUrl;
            SelectedGenres = _selectedGenres;
        }
        public AddGameViewModel(List<Genre> _genres)
        {
            Genres = _genres;
        }
    }
}
