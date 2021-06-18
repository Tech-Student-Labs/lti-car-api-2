using Microsoft.AspNetCore.Mvc;
using VehicleWebAPI.Services;
using VehicleWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

namespace VehicleWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserDatabaseService _service;
        private readonly IUserAuthenticationService _authService;

        public UserController(IUserDatabaseService userDatabaseService, IUserAuthenticationService authService)
        {
            _service = userDatabaseService;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_service.GetAllUsers());
        }

        [HttpGet]
        [Route("Profile")]
        [Authorize]
        public IActionResult GetUserById()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.Claims.FirstOrDefault(u => u.Type == "id").Value;
            return Ok(_service.GetUserById(int.Parse(userId)));
        }

        [HttpPost]
        public IActionResult createUser([FromBody] User user)
        {
            if (_service.EmailExists(user.Email)) {
                return Conflict("User could not be added, email already in use.");
            }

            if (_service.UsernameExists(user.UserName)) {
                return Conflict("User could not be added, username already in use.");
            }

            var u = _service.AddUser(user);
            var t = _authService.Authenticate(user);

            return Created($"/user/{u.Id}", t);
        }
    }
}