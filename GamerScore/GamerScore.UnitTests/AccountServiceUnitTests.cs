using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.Core;
using Moq;
using Gamerscore.Core.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gamerscore.DTO;
using Gamerscore.DTO.Enums;
using Microsoft.AspNetCore.Identity;

namespace GamerScore.UnitTests
{
    [TestClass]
    public class AccountServiceUnitTests
    {
        private Mock<IAccountRepository> _mockAccountRepository;
        private AccountService _accountService;

        [TestInitialize]
        public void Setup()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _accountService = new AccountService(_mockAccountRepository.Object);
        }

        [TestMethod]
        public void CreateAccount_AccountCreated()
        {
            //Arrange
            string username = "Henk";
            string email = "Henk@gmail.com";
            string password = "Henk123"
;            
            _mockAccountRepository.Setup(repo => repo.CreateUser(username,email,password)).Returns(true);

            //Act
            bool result = _accountService.CreateAccount(username,email,password);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckLogin_LoginSuccess()
        {
            //Arrange
            string email = "Admin@gmail.com";
            UserRole userRole = UserRole.Admin;
            string password = "password";
            User user = new User(email);

            PasswordHasher<User> passwordHasher = new();
            string hashedPassword = passwordHasher.HashPassword(user, password);
            _mockAccountRepository.Setup(repo => repo.GetPasswordHash(email)).Returns(hashedPassword);
            _mockAccountRepository.Setup(repo => repo.GetAccountInfo(email)).Returns(new User(1, userRole));
            

            //Act
            bool result;
            int accountIdResult;
            UserRole userRoleResult;

            (result, accountIdResult, userRoleResult) = _accountService.CheckLogin(email, password);

            //Arrange
            Assert.IsTrue(result);
        }

    }
}
