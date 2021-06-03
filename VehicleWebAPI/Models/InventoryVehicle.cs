using System.ComponentModel.DataAnnotations.Schema;
using VehicleWebAPI.Interfaces;

namespace VehicleWebAPI.Models
{
    public class InventoryVehicle : IInventoryVehicle
    {
        public int Id { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        public double Price { get; set; }
    }
}