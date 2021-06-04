using Microsoft.AspNetCore.Mvc;

namespace VehicleWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Vehicle : ControllerBase
    {
        // GET
        public IActionResult GetAllVehicles()
        {
            return Ok();
        }
    }
}