using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VehicleWebAPI.Models;
using VehicleWebAPI.Services;

namespace VehicleWebAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class VehicleController : ControllerBase
  {
    private readonly IVehicleDatabaseService _service;

    public VehicleController(IVehicleDatabaseService service)
    {
      _service = service;
    }

    // GET
    [HttpGet]
    public IActionResult GetAllVehicles()
    {
      return Ok(_service.GetAllVehicles());
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetVehicleById([FromRoute] int id)
    {
      return Ok(_service.ReadVehicleById(id));
    }

    [HttpPost]
    public IActionResult CreateVehicle([FromBody] Vehicle vehicle)
    {
      return _service.CreateVehicle(vehicle) == 1
          ? Created($"/{vehicle.Id}"
              , new { vehicle.Id, vehicle.Make, vehicle.Model, vehicle.VIN })
          : StatusCode(400, "Vehicle could not be added. Try not including a vehicle id.");
    }

    [HttpPut]
    public IActionResult UpdateVehicle([FromBody] Vehicle vehicle)
    {
      return _service.UpdateVehicle(vehicle) == 1
          ? Ok(new { vehicle.Id, vehicle.Make, vehicle.Model, vehicle.VIN })
          : StatusCode(400, "Vehicle could not be updated.");
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteVehicle([FromRoute] int id)
    {
      return _service.DeleteVehicleById(id) == 1
          ? Ok()
          : StatusCode(400, "Vehicle could not be deleted.");
    }

    // [HttpPost]
    // [Route("{id:int}")]
    // public IActionResult CreateVehicleImage([FromRoute] int id, [FromForm] VehicleImageRequest vehicleImageRequest)
    // {
    //   return _service.CreateVehicleImages(id, vehicleImageRequest) == 1 ? Ok() : StatusCode(500);
    // }
    //
  }
}