using Gamerscore.Core.Interfaces;
using Gamerscore.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Gamerscore.Core
{

    
    public class LoginManager
    {
        public bool CreateAccount(IAccountDB _accountDB, string _username, string _email, string _password)
        {
            User user = new(_username, _email);
            PasswordHasher<User> passwordHasher = new();
                passwordHasher.HashPassword()
            _accountDB.CreateUser(_username, _email, _password);
        }

    }
}
