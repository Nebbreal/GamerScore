namespace Gamerscore.Core.Interfaces
{
    public interface IGameRepository
    {
        public bool CreateGame(string _title, string description, string developer, string _thumbnailImageUrl, List<string> _imageUrls, List<int> _genreIds);
    }
}
