using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using VehicleWebAPI.Models;
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


        [HttpGet]
        public IActionResult GetListOfInventoryVehicles()
        {
            return Ok(_service.GetInventoryVehicles());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetInventoryVehicleById([FromRoute] int id)
        {
            var vehicle = _service.GetInventoryVehicleById(id);
            return vehicle switch
            {
                null => StatusCode(404),
                _ => Ok(vehicle)
            };
        }
        
    }
}