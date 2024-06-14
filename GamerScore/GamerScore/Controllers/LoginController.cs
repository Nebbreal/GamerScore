using Gamerscore.Core.Interfaces.Services;
using Gamerscore.DTO.Enums;
using GamerScore.Models;
using GamerScore.Options;
using GamerScore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GamerScore.Controllers
{
    public class LoginController : Controller
    {
        private IAccountService accountService;
        private ITokenService tokenService;

        public LoginController(IAccountService _accountService, ITokenService _tokenService)
        {
            accountService = _accountService;
            tokenService = _tokenService;

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
               // AccountService loginManager = new(accountRepository);

                bool loginResult;
                int accountId;
                UserRole role;

                (loginResult, accountId, role) = accountService.CheckLogin(_LoginViewModel.Email, _LoginViewModel.Password);
                if (loginResult)
                {
                    //Create jwt token
                    int expirationTime = 12;

                    var token = tokenService.CreateJwt(_LoginViewModel.Email, accountId, role, expirationTime);

                    Response.Cookies.Append("jwtToken", token, new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddHours(expirationTime),
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
            if(accountService.CreateAccount(_SignUpViewModel.Username, _SignUpViewModel.Email, _SignUpViewModel.Password))//ToDo: is there a better way to do this? There is, throwing exceptions
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
