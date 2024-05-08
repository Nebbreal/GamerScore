using Gamerscore.Core.Models;

namespace GamerScore.Models
{
    public class AddGameViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string>? ImageUrl { get; set; }
        public List<string>? SelectedGenres {  get; set; }
        public List<Genre>? Genres { get; private set; }


        public AddGameViewModel() { }
        public AddGameViewModel(string _name, string _description, List<string> _ImageUrl, List<string> _selectedGenres)
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
