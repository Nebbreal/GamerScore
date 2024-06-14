using Gamerscore.DTO.Enums;

namespace Gamerscore.Core.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateJwt(string _email, int _accountId, UserRole _role, int _expirationTime);
        bool ValidateAdminLevelJwt(string token);
        bool ValidateUserLevelJwt(string token);
    }
}