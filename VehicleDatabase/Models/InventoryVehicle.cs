using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace VehicleDatabase.Models
{
    public class InventoryVehicle : IInventoryVehicle
    {
        public int Id { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        public double Price { get; set; }
    }
}