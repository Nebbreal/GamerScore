using Gamerscore.DTO.Enums;
using Gamerscore.Core.Interfaces;
using Gamerscore.DTO;
using Microsoft.AspNetCore.Identity;

namespace Gamerscore.Core
{
    public class AccountManager
    {
        private IAccountRepository accountRepository;
        public AccountManager(IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
        }

        public bool CreateAccount(string _username, string _email, string _password)
        {
            User user = new(_username, _email);
            PasswordHasher<User> passwordHasher = new();
            string hashedPassword = passwordHasher.HashPassword(user, _password);

            accountRepository.CreateUser(_username, _email, hashedPassword);

            return true;
        }

        //Takes the email and password to check if the user is allowde to login, if the log in is succesful we pass the accountId and role to use for a Jwt token later
        public (bool result, int accountId, UserRole role) CheckLogin(string _email, string _password)
        {
            string passwordHash = accountRepository.GetPasswordHash(_email);

            if (passwordHash == "Password not found")
            {
                return (false, -1, UserRole.None);
            }
            else
            {
                User user = new(_email);
                PasswordHasher<User> passwordHasher = new();
                PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(user, passwordHash, _password);
               
                if(result == PasswordVerificationResult.Success)
                {
                    User userInfo = accountRepository.GetAccountInfo(_email);
                    if (userInfo.Id == null)
                    {
                        return (false, -1, UserRole.None);
                    }
                    return (true, (int)userInfo.Id, userInfo.Role);
                }
                else
                {
                    return (false, -1, UserRole.None);
                }
            }
        }
    }
}
