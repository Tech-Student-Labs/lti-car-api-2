using VehicleDatabase.Models;

namespace VehicleDatabase.Interfaces
{
    public interface IVehicleToImage
    {
        int VehicleId { get; set; }
        Vehicle Vehicle { get; set; }
        int VehicleImageId { get; set; }
        VehicleImage VehicleImage { get; set; }
    }
}