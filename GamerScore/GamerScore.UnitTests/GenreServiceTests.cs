using Gamerscore.Core;
using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.DTO;
using Moq;

namespace GamerScore.UnitTests
{
    [TestClass]
    public class GenreServiceTests
    {
        private Mock<IGenreRepository> _mockGenreRepository;
        private GenreService _genreService;

        [TestInitialize]
        public void Setup()
        {
            _mockGenreRepository = new Mock<IGenreRepository>();
            _genreService = new GenreService(_mockGenreRepository.Object);
        }

        [TestMethod]
        public void CreateGenre_NameAlreadyExists()
        {
            // Arrange
            _mockGenreRepository.Setup(repo => repo.GetGenreByName("Sports")).Returns(new Genre(1, "Sports", "image_url"));

            // Act
            var result = _genreService.CreateGenre("Sports", "image_url");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateGenre_GenreCreated()
        {
            // Arrange
            _mockGenreRepository.Setup(repo => repo.GetGenreByName("Shooter")).Returns(new Genre(1, "Sports", "image_url"));
            _mockGenreRepository.Setup(repo => repo.CreateGenre(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            // Act
            var result = _genreService.CreateGenre("Shooter", "image_url");

            // Assert
            Assert.IsTrue(result);
        }
    }
}