using System.ComponentModel.DataAnnotations.Schema;
using VehicleWebAPI.Interfaces;

namespace VehicleWebAPI.Models
{
    public class VehicleImage : IVehicleImage
    {
        public int Id { get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public byte[] ImageData { get; set; }
    }
}