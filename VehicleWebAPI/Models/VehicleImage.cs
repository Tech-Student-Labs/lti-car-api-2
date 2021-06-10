using System.ComponentModel.DataAnnotations.Schema;
using VehicleWebAPI.Interfaces;

namespace VehicleWebAPI.Models
{
    public class VehicleImage : IVehicleImage
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        // public Vehicle Vehicle { get; set; }
        public string ImageData { get; set; }
    }
}