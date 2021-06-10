﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
  public interface IVehicleDatabaseService
  {
    int CreateVehicle(Vehicle vehicle, IFormFileCollection images);
    Vehicle ReadVehicleById(int id);
    int UpdateVehicle(Vehicle vehicle);
    int DeleteVehicleById(int id);
    List<Vehicle> GetAllVehicles();
  }
}