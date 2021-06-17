using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VehicleWebAPI.Models;
using VehicleWebAPI.Services;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserDatabaseService _service;

    public AuthController(IUserDatabaseService service) {
        _service = service;
    }

    // GET api/values
    [HttpPost, Route("login")]
    public IActionResult Login([FromBody]User user)
    {
        if (user == null)
        {
            return BadRequest("Invalid client request");
        }

        var dbUser = _service.VerifyCredentials(user);
        //if (user.UserName == "johndoe" && user.Password == "def@123")
        if (dbUser is not null)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenOptions = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:5001",
                Audience = "https://localhost:5001",
                Subject = new ClaimsIdentity( new[] {new Claim("username", dbUser.UserName), new Claim("id", dbUser.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = signinCredentials,

            };

            var token = tokenHandler.CreateToken(tokenOptions);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new { Token = tokenString });
        }
        else
        {
            return Unauthorized();
        }
    }
}