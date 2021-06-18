using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        public string Authenticate(User user)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenOptions = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:5001",
                Audience = "https://localhost:5001",
                Subject = new ClaimsIdentity(new[] { new Claim("username", user.UserName), new Claim("id", user.Id.ToString()) }),
                Expires = System.DateTime.UtcNow.AddDays(2),
                SigningCredentials = signinCredentials,

            };

            var token = tokenHandler.CreateToken(tokenOptions);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}