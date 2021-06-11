using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleWebAPI.Services;

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
        public IActionResult createUser()
        {
            return Ok();
        }
    }
}