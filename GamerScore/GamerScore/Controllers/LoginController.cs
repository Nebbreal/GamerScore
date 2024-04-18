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
        
        public LoginController(IOptions<ConnectionStrings> connectionStrings)
        {
            this._connectionStrings = connectionStrings.Value;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string _email, string _password)
        { 
            return View(); 
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            AccountDB accountDB = new(_connectionStrings.DBConnectionString);
            LoginManager loginManager = new();
            //accountDB.CreateUser(model.Username, model.Email, model.Password);
            return RedirectToAction("Login");
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
