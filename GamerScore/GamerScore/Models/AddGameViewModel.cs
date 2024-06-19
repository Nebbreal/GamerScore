using Gamerscore.DTO;

namespace GamerScore.Models
{
    public class AddGameViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public List<string>? ImageUrl { get; set; }
        public List<string>? SelectedGenres {  get; set; }
        public List<Genre>? Genres { get; set; }


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
