using VehicleDatabase.Models;

namespace VehicleDatabase.Interfaces
{
    public interface IInventoryVehicle
    {
        int Id { get; set; }
        int VehicleId { get; set; }
        Vehicle Vehicle { get; set; }
        double Price { get; set; }
    }
}