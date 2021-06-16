using System.Collections.Generic;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Interfaces
{
    public interface IVehicle : IResponseVehicle
    {
        User User { get; set; }
        int UserId { get; set; }
    }
}