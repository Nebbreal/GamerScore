using Gamerscore.Core;
using GamerScore.DAL;
using GamerScore.Models;
using GamerScore.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
    }
}
