using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using sa_login.Models;

namespace sa_login.Controllers
{
    public class SecurityController : Controller
    {
        
        public IActionResult Login(string requestPath)
        {
            ViewBag.Request = requestPath??"/";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel login)
        {
            if(!IsAuthentic(login.Email,login.Password))
                return View();
            List<Claim> claims = new List<Claim>(){
                new Claim(ClaimTypes.Name,"Bob Rich"),
                new Claim(ClaimTypes.Email,login.Email)
            };

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

        private bool IsAuthentic(string userName,string password)
        {
            return (userName=="stone" && password=="1234");
        }
    }
}