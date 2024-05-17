using Gamerscore.Core;
using Gamerscore.Core.Interfaces;
using Gamerscore.DTO.Enums;
using GamerScore.DAL;
using GamerScore.Models;
using GamerScore.Options;
using GamerScore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GamerScore.Controllers
{
    public class LoginController : Controller
    {
        private IAccountRepository accountRepository;
        private readonly JwtSettings jwtSettings;
        public LoginController(IAccountRepository accountRepository, IOptions<JwtSettings> jwt)
        {
            this.accountRepository = accountRepository;
            this.jwtSettings = jwt.Value;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel _LoginViewModel)
        {
            //Model validation
            if(_LoginViewModel.Email.Length < 1 || _LoginViewModel.Password.Length < 1)
            {
                string error = "Email or password is missing";
                _LoginViewModel.ErrorMessage = error;
                return View(_LoginViewModel);
            }
            else
            {
                AccountManager loginManager = new(accountRepository);

                bool loginResult;
                int accountId;
                UserRole role;

                (loginResult, accountId, role) = loginManager.CheckLogin(_LoginViewModel.Email, _LoginViewModel.Password);
                if (loginResult)
                {
                    //Create jwt token
                    int expirationTime = 10;

                    TokenService tokenService = new(jwtSettings);
                    var token = tokenService.CreateJwt(_LoginViewModel.Email, accountId, role, expirationTime);

                    Response.Cookies.Append("jwtToken", token, new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddMinutes(expirationTime),
                        HttpOnly = true //Cookie can only be found in an http request
                    });

                    return RedirectToAction("Home", "Home");
                }
                else
                {   
                    string error = "Email or password is incorrect";
                    _LoginViewModel.ErrorMessage = error;
                    return View(_LoginViewModel);
                }
            }
        }
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwtToken");
            return RedirectToAction("Home", "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel _SignUpViewModel)
        {
            AccountManager loginManager = new(accountRepository);
            if(loginManager.CreateAccount(_SignUpViewModel.Username, _SignUpViewModel.Email, _SignUpViewModel.Password))//ToDo: is there a better way to do this? There is, throwing exceptions
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
