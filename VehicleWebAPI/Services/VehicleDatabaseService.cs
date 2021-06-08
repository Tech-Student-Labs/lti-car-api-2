using System.Collections.Generic;
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
            _db.Add(vehicle);
            _db.SaveChanges();
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