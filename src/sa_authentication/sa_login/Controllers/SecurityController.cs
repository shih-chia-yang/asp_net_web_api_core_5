using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sa_login.Domain;
using sa_login.Models;
using sa_login.Repositories;

namespace sa_login.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IUserRepository _userRepo;

        public SecurityController(IUserRepository repo)
        {
            _userRepo = repo;
        }
        
        public IActionResult Login(string requestPath)
        {
            ViewBag.Request = requestPath??"/";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel login)
        {
            var loginUser =IsAuthentic(login.Email,login.Password);
            if(loginUser==null)
                return View();
            HttpContext.Session.SetString("userName", loginUser.Name);
            List<Claim> claims = new List<Claim>(){
                new Claim(ClaimTypes.Name,"Bob Rich"),
                new Claim(ClaimTypes.Email,login.Email),
                new Claim("Id",loginUser.Id.ToString()),
                new Claim("HasExpenseCredit",loginUser.HasExpenseCredit.ToString())
            };

            if(loginUser.CanManaged)
                claims.Add(new Claim("CanManaged",loginUser.CanManaged.ToString()));


            ClaimsIdentity identity = new ClaimsIdentity(claims,"cookie");
            ClaimsPrincipal principal =new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                scheme:"SaSecurityScheme",
                principal:principal,
                properties:new AuthenticationProperties
                {
                    IsPersistent=true,// for remember me feature
                    ExpiresUtc=DateTime.UtcNow.AddMinutes(15) 
                });
            return Redirect(login.RequestPath??"/");
        }

        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(scheme:"SaSecurityScheme");
            return RedirectToAction("Login");
        }

        public IActionResult Access()
        {
            return View();
        }

        private User IsAuthentic(string userName,string password)
        {
            return _userRepo.Find(userName,password);
        }
    }
}