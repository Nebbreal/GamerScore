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
            string hashedPassword = passwordHasher.HashPassword(user, _password);

            _accountDB.CreateUser(_username, _email, hashedPassword);

            return true;
        }

        public bool Login(IAccountDB _accountDB, string _email, string _password)
        {
            string passwordHash = _accountDB.GetPasswordHash(_email);

            if (passwordHash == "Password not found") //ToDo: is there a better way to do this part 2?
            {
                return false;
            }
            else
            {
                User user = new(_email);
                PasswordHasher<User> passwordHasher = new();
                PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, passwordHash, _password);

                if(result == PasswordVerificationResult.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
