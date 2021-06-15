using Microsoft.AspNetCore.Mvc;
using VehicleWebAPI.Services;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserDatabaseService _userDatabaseService;

        public UserController(IUserDatabaseService userDatabaseService)
        {
            _userDatabaseService = userDatabaseService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userDatabaseService.GetAllUsers());
        }

        [HttpPost]
        public IActionResult createUser([FromBody] User user)
        {
            if (_userDatabaseService.EmailExists(user.Email)) {
                return Conflict("User could not be added, email already in use.");
            }

            if (_userDatabaseService.UsernameExists(user.UserName)) {
                return Conflict("User could not be added, username already in use.");
            }

            var u = _userDatabaseService.AddUser(user);

            return Created($"/user/{u.Id}", u);
        }
    }
}