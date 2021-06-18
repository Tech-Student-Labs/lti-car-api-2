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
    private readonly IUserAuthenticationService _authService;

    public AuthController(IUserDatabaseService service, IUserAuthenticationService authService) {
        _service = service;
        _authService = authService;
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
        if (dbUser is not null)
        {
            var tokenString = _authService.Authenticate(dbUser);
            return Ok(new { Token = tokenString });
        }
        else
        {
            return Unauthorized();
        }
    }
}