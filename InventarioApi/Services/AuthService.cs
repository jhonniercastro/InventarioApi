using InventarioApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventarioCCL.API.Services;
public class AuthService
{
    private readonly IConfiguration _config;

    private readonly List<UserModel> _users = new()
        {
            new UserModel { Username = "admin", Password = "admin123" },
            new UserModel { Username = "usuario", Password = "password" }
        };

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string? Authenticate(UserModel user)
    {
        var validUser = _users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
        if (validUser == null)
            return null;

        return GenerateJwtToken(validUser);
    }

    private string GenerateJwtToken(UserModel user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(ClaimTypes.Name, user.Username)
            };

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpireMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
