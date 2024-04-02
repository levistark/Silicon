using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Silicon_API.Filters;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Silicon_API.Controllers;
[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    public IActionResult GetToken()
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return Unauthorized();
    }
}
