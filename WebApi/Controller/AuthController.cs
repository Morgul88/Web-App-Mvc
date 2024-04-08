using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using WebApi.Filters;
using WebApi.Model;

namespace WebApi.Controller
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(DataContext dataContext, IConfiguration configuration) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IConfiguration _configuration = configuration;



        
        [HttpPost]
        [Route("token")]
        public IActionResult GetToken(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new(ClaimTypes.Email, model.Email),
                        new(ClaimTypes.Email, model.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(tokenString);
            }

            return Unauthorized();
        }
    }
}
