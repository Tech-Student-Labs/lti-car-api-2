using System.Collections.Generic;

namespace Models
{
    public interface IVehicle
    {
        string VIN { get; set; }
        string Make { get; set; }
        string Model { get; set; }
        string Year { get; set; }
        int Miles { get; set; }
        string Color { get; set; }
        List<VehicleImage> Images { get; set; }
        double SellingPrice { get; set; }
        User User { get; set; }
        int UserId { get; set; }
        Vehicle.StatusCode Status { get; set; }
    }
}