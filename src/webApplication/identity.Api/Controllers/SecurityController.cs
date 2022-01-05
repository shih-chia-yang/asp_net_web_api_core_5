using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using identity.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace identity.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        [HttpPost]
        public IActionResult GenerateToken([FromBody]LoginViewModel model)
        {
            if(model.Email !="stone" && model.Password !="1234")
                return Unauthorized();
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub,"stone"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("MemberId","100")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SmartIt-secret-key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("Heip.Security.Jwt",
            "Heip.Security.Jwt",
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials:creds);
            return Ok(new {token=new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}