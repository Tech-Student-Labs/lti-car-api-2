using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VehicleDatabase.Data;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
    public class InventoryDatabaseService : IInventoryDatabaseService
    {
        private readonly DatabaseContext _db;

        public InventoryDatabaseService(DatabaseContext db)
        {
            _db = db;
        }
        
        
        public InventoryVehicle GetInventoryVehicleById(int id)
        {
            var vehicle = _db.Inventory.Include(v => v.Vehicle).FirstOrDefault(v => v.VehicleId == id);
            return vehicle;
        }

        public List<InventoryVehicle> GetInventoryVehicles()
        {
            return _db.Inventory.Include(v => v.Vehicle).ToList();
        }
    }
}