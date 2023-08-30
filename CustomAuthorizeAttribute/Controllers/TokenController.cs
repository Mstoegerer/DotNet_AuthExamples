using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CustomAuthorizeAttribute.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenController
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        IConfiguration configuration = new ConfigurationManager()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile("appsettings.Development.json", true)
            .AddJsonFile("appsettings.Local.json", true)
            .AddEnvironmentVariables()
            .Build();
        var jwtConfig = configuration.GetSection("Jwt").Get<JwtConfig>();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, "mstoegerer"),
                new Claim(JwtRegisteredClaimNames.Email, "dev@mstoegerer.net"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            }),
            Expires = DateTime.UtcNow.AddMinutes(jwtConfig.ExpiryInMinutes),
            Issuer = jwtConfig.Issuer,
            Audience = jwtConfig.Audience,
            SigningCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(jwtConfig.Key.ToUTF8Bytes()),
                SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }
}