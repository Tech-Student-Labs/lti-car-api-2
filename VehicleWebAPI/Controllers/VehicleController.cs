using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using VehicleWebAPI.Models;
using VehicleWebAPI.Services;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;

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

        [HttpGet]
        [Route("History")]
        [Authorize]
        public IActionResult GetVehiclesByUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = identity.Claims.FirstOrDefault(u => u.Type == "username").Value;
            return Ok(_service.GetVehicleByUsername(username));
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateVehicle([FromBody] Vehicle vehicle)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.Claims.FirstOrDefault(u => u.Type == "id").Value;
            vehicle.UserId = Convert.ToInt32(userId);
            return _service.CreateVehicle(vehicle) == 1
                ? Created($"vehicle/{vehicle.Id}"
                    , new {vehicle.Id, vehicle.Make, vehicle.Model, vehicle.VIN})
                : StatusCode(400, "Vehicle could not be added. Try not including a vehicle id.");
        }

        [HttpPut]
        public IActionResult UpdateVehicle([FromBody] Vehicle vehicle)
        {
            return _service.UpdateVehicle(vehicle) == 1
                ? Ok(new {vehicle.Id, vehicle.Make, vehicle.Model, vehicle.VIN})
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
    }
}