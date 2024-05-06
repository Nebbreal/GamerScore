namespace GamerScore.Models
{
    public class AddGenreViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }
}
