using Gamerscore.Core;
using Gamerscore.Core.Enums;
using GamerScore.DAL;
using GamerScore.Models;
using GamerScore.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GamerScore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly JwtSettings _jwtSettings;
        public LoginController(IOptions<ConnectionStrings> connectionStrings, IOptions<JwtSettings> jwt)
        {
            this._connectionStrings = connectionStrings.Value;
            this._jwtSettings = jwt.Value;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel _model)
        {
            //Model validation
            if(_model.Email.Length < 1 || _model.Password.Length < 1)
            {
                string error = "Email or password is missing";
                _model.ErrorMessage = error;
                return View(_model);
            }
            else
            {
                AccountDB accountDB = new(_connectionStrings.DBConnectionString);
                LoginManager loginManager = new();

                bool loginResult;
                int accountId;
                UserRole role;

                (loginResult, accountId, role) = loginManager.checkLogin(accountDB, _model.Email, _model.Password);
                if (loginResult)
                {
                    //Create jwt token
                    CreateJwt(_model.Email, accountId, role);

                    return RedirectToAction("Home", "Home");
                }
                else
                {   
                    string error = "Email or password is incorrect";
                    _model.ErrorMessage = error;
                    return View(_model);
                }
            }
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel _model)
        {
            AccountDB accountDB = new(_connectionStrings.DBConnectionString);
            LoginManager loginManager = new();
            if(loginManager.CreateAccount(accountDB, _model.Username, _model.Email, _model.Password))//ToDo: is there a better way to do this?
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        public IActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        IActionResult PasswordReset(string _email)
        {
            return View();
        }

        private void CreateJwt(string _email, int _accountId, UserRole _role)
        {
            int expirationTime = 1;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(
                $"{_jwtSettings.Key}");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Email", _email),
                    new Claim("AccountId", _accountId.ToString()),
                    new Claim("Role", _role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenstring = tokenHandler.WriteToken(token);
            Response.Cookies.Append("jwtToken", tokenstring, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(expirationTime),
                HttpOnly = true //Cookie can only be found in an http request
            });
        }
    }
}
