using System.Collections.Generic;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Interfaces
{
    public interface IVehicle
    {
        int Id { get; set; }
        string VIN { get; set; }
        string Make { get; set; }
        string Model { get; set; }
        string Year { get; set; }
        int Miles { get; set; }
        string Color { get; set; }
        ICollection<VehicleImage> VehicleImages { get; set; }
        double SellingPrice { get; set; }
        User User { get; set; }
        int UserId { get; set; }
        Vehicle.StatusCode Status { get; set; }
    }
}