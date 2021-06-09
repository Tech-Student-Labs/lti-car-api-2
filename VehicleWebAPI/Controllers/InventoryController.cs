using Microsoft.AspNetCore.Mvc;
using VehicleWebAPI.Services;

namespace VehicleWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryDatabaseService _service;

        public InventoryController(IInventoryDatabaseService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetInventoryVehicleById([FromRoute] int id)
        {
            return Ok();
        }
        
    }
}