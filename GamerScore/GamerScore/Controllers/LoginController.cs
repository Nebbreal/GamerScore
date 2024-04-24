using Gamerscore.Core;
using Gamerscore.Core.Models;
using GamerScore.DAL;
using GamerScore.Models;
using GamerScore.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
            if(_model.Email.Length < 1 || _model.Password.Length < 0)
            {
                string error = "Email or password is missing";
                _model.ErrorMessage = error;
                return View(_model);
            }
            else
            {
                AccountDB accountDB = new(_connectionStrings.DBConnectionString);
                LoginManager loginManager = new();
                if (loginManager.Login(accountDB, _model.Email, _model.Password))
                {
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

        //private void CreateJwt(string _email, string _hashedPassword, string _accountId)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(
        //        "");
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim("Email", email),
        //            new Claim("AccountId", u.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(expirationTime),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //            SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenstring = tokenHandler.WriteToken(token);
        //    Response.Cookies.Append("jwtToken", tokenstring, new CookieOptions
        //    {
        //        Expires = DateTime.UtcNow.AddDays(expirationTime),
        //        HttpOnly = true //Cookie can only be found in an http request
        //    });
        //}
    }
}
