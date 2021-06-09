using System.ComponentModel.DataAnnotations.Schema;
using VehicleWebAPI.Interfaces;

namespace VehicleWebAPI.Models
{
    public class InventoryVehicle : IInventoryVehicle
    {
        public int Id { get; set; }
        
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public double Price { get; set; }
        
    }
}