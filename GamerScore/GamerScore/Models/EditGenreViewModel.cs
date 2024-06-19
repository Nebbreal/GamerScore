using Gamerscore.DTO;
using System.ComponentModel.DataAnnotations;

namespace GamerScore.Models
{
    public class EditGenreViewModel
    {
        public List<Genre>? AllGenres {  get; set; }
        public string? GenreImageUrl { get; set; }

        [Required(ErrorMessage = "You must select a genre.")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "You must enter a name.")]
        public string GenreName { get; set; }

        
        public EditGenreViewModel() { }
    }
}
