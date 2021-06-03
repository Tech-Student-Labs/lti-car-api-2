using System.ComponentModel.DataAnnotations.Schema;
using VehicleDatabase.Interfaces;

namespace VehicleDatabase.Models
{
    public class VehicleImage : IVehicleImage
    {
        public int Id { get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public byte[] ImageData { get; set; }
    }
}