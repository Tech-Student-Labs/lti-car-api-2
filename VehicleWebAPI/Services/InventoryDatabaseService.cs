using System.Linq;
using Microsoft.EntityFrameworkCore;
using VehicleDatabase.Data;

namespace VehicleWebAPI.Services
{
    public class InventoryDatabaseService : IInventoryDatabaseService
    {
        private readonly DatabaseContext _db;

        public InventoryDatabaseService(DatabaseContext db)
        {
            _db = db;
        }
        
        
        public object GetInventoryVehicleById(int id)
        {
            var vehicle = _db.Inventory.Include(v => v.Vehicle).FirstOrDefault(v => v.VehicleId == id);
            return vehicle;
        }
    }
}