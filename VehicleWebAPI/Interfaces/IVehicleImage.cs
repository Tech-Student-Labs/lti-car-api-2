using System.ComponentModel.DataAnnotations;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Interfaces
{
    public interface IVehicleImage
    {
        int Id { get; set; }
        int VehicleId { get; set; }
        
        Vehicle Vehicle { get; set; }
        byte[] ImageData { get; set; }
    }
}