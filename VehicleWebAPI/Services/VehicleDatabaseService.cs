﻿using System.Collections.Generic;
using System.Linq;
using VehicleDatabase.Data;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
  public class VehicleDatabaseService : IVehicleDatabaseService
  {
    private readonly DatabaseContext _db;

    public VehicleDatabaseService(DatabaseContext db)
    {
      _db = db;
    }

    //CREATE
    public int CreateVehicle(Vehicle vehicle)
    {
      if (vehicle.VIN == null
      || vehicle.Make == null
      || vehicle.Model == null
      || vehicle.Year == null
      || vehicle.Miles < 0
      || vehicle.Color == null
      || vehicle.SellingPrice < 0
      || vehicle.Status < 0
      || vehicle.UserId < 0) return 0;
      if (_db.Vehicles.FirstOrDefault(t => t.VIN == vehicle.VIN) != null) return 0;
      try
      {
        _db.Add(vehicle);
        _db.SaveChanges();
      }
      catch (System.ArgumentException)
      {
        return 0;
      }
      return 1;
    }

    //READ
    public Vehicle ReadVehicleById(int id)
    {
      var vehicle = _db.Vehicles.FirstOrDefault(v => v.Id == id);
      return vehicle;
    }

    //UPDATE
    public int UpdateVehicle(Vehicle vehicle)
    {
      _db.Update(vehicle);
      _db.SaveChanges();
      return 1;
    }

    //DELETE
    public int DeleteVehicleById(int id)
    {
      _db.Remove(ReadVehicleById(id));
      _db.SaveChanges();
      return 1;
    }

    //LIST
    public List<Vehicle> GetAllVehicles()
    {
      return _db.Vehicles.Take(20).ToList();
    }
  }
}