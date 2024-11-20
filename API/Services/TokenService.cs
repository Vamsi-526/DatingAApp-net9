using System;
using API.Entities;
using API.Interfaces;
namespace API.Services;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenkey = config["TOkenKey"] ?? throw new Exception("Cannot access tokenkey from appsettings");
        if(tokenkey.Length < 64) throw new Exception("Your Token Key Needs To be Longer");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));
        
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.UserName)
        };
        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
           Subject = new ClaimsIdentity(claims),
           Expires = DateTime.UtcNow.AddDays(7),
           SigningCredentials = creds

        };
        var tokenHandler=  new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);


    }
}
