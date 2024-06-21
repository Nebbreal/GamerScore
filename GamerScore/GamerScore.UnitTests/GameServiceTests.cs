using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.Core;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GamerScore.Domain;
using GamerScore.Exceptions;

namespace GamerScore.UnitTests
{
    [TestClass]
    public class GameServiceTests
    {
        private Mock<IGameRepository> _mockGameRepository;
        private GameService _gameService;

        [TestInitialize]
        public void Setup()
        {
            _mockGameRepository = new Mock<IGameRepository>();
            _gameService = new GameService(_mockGameRepository.Object);
        }

        [TestMethod]
        public void GetGameById_ReturnsInvalidObject()
        {
            //Arrange
            Game expectedGame = new Game(1, "Breath of the wild", "Open world game", "Nintendo", "image.png");
            _mockGameRepository.Setup(repo => repo.GetGameById(1)).Returns(new Game(1, "Breath of the wild", "Open world game", "Nintendo", "image.png"));
            //Act
            Game gameResult = _gameService.GetGameById(1);
            //Assert
            Assert.AreEqual(expectedGame.Title, gameResult.Title);
            Assert.AreEqual(expectedGame.Description, gameResult.Description);
            Assert.AreEqual(expectedGame.ThumbnailImageUrl, gameResult.ThumbnailImageUrl);
            Assert.AreEqual(expectedGame.Developer, gameResult.Developer);
        }

        [TestMethod]
        public void GetGameById_ReturnsNullGame()
        {
            //Arrange
            Game expectedGame = new Game(1, "Failed fetching game", "Failed fetching game", "Failed fetching game", "Failed fetching game");
            _mockGameRepository.Setup(repo => repo.GetGameById(1)).Throws(new DataFetchFailedException());
            //Act
            Game gameResult = _gameService.GetGameById(1);
            //Assert
            Assert.AreEqual(expectedGame.Title, gameResult.Title);
            Assert.AreEqual(expectedGame.Description, gameResult.Description);
            Assert.AreEqual(expectedGame.ThumbnailImageUrl, gameResult.ThumbnailImageUrl);
            Assert.AreEqual(expectedGame.Developer, gameResult.Developer);

        }
    }
}
