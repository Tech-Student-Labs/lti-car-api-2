using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using VehicleDatabase.Interfaces;
using VehicleDatabase.Models;

namespace Models
{
    public class InventoryVehicle
    {
        public int Id { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        public double Price { get; set; }
    }
}