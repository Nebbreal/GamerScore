using Gamerscore.DTO.Enums;

namespace Gamerscore.Core.Interfaces.Services
{
    public interface IAccountService
    {
        (bool result, int accountId, UserRole role) CheckLogin(string _email, string _password);
        bool CreateAccount(string _username, string _email, string _password);
    }
}