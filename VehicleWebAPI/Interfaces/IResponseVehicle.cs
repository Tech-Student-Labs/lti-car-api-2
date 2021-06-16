using System.Collections.Generic;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Interfaces
{
    public interface IResponseVehicle
    {
         int Id { get; set; }
        string VIN { get; set; }
        string Make { get; set; }
        string Model { get; set; }
        int Year { get; set; }
        int Miles { get; set; }
        string Color { get; set; }
        List<VehicleImage> VehicleImages { get; set; }
        double SellingPrice { get; set; }
        ResponseVehicle.StatusCode Status { get; set; }
    }
}