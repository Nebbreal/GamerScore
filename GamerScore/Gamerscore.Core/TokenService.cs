using Gamerscore.DTO.Enums;
using Gamerscore.Core.Interfaces;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace GamerScore.Services
{
    public class TokenService
    {
        private readonly IJwtSettings _jwtSettings;

        public TokenService(IJwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string CreateJwt(string _email, int _accountId, UserRole _role, int _expirationTime)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Email", _email),
                    new Claim("AccountId", _accountId.ToString()),
                    new Claim("Role", _role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_expirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateUserLevelJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;

            }
            catch
            {
                // Token validation failed
                return false;
            }
        }

        public bool ValidateAdminLevelJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // Set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var role = jwtToken.Claims.First(x => x.Type == "Role").Value;

                // Check if the role is Admin
                if (role == UserRole.Admin.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                // Token validation failed
                return false;
            }
        }
    }
}
