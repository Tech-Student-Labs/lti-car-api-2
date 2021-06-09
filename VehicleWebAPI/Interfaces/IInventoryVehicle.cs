using Microsoft.EntityFrameworkCore;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Interfaces
{
    public interface IInventoryVehicle
    {
        int Id { get; set; }
        int VehicleId { get; set; }
        Vehicle Vehicle { get; set; }
        double Price { get; set; }
    }
}