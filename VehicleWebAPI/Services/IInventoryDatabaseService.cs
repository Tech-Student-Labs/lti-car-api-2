using System.Collections.Generic;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
    public interface IInventoryDatabaseService
    {
        public InventoryVehicle GetInventoryVehicleById(int id);
        List<InventoryVehicle> GetInventoryVehicles();
    }
}