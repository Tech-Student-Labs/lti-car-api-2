using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using VehicleDatabase.Interfaces;

namespace VehicleDatabase.Models
{
    public class VehicleImage
    {
        public int Id { get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public byte[] ImageData { get; set; }
    }
}