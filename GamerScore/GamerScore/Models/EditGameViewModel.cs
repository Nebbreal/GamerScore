using Gamerscore.DTO;
using GamerScore.DTO;
using System.ComponentModel.DataAnnotations;

namespace GamerScore.Models
{
    public class EditGameViewModel
    {
        public List<Game>? AllGames { get; set; }
        public List<string>? ImageUrl { get; set; }
        public List<string>? SelectedGenres { get; set; }
        public List<Genre>? Genres { get; set; }

        [Required (ErrorMessage = "Please select a game")]
        public int GameId { get; set; }

        [Required (ErrorMessage = "A name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A developer is required")]
        public string Developer { get; set; }

        [Required(ErrorMessage = "A thumbnail image is required")]
        public string ThumbnailImageUrl { get; set; }

        public EditGameViewModel() { }
    }
}
