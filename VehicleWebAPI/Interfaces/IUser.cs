using System.Collections.Generic;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        List<Vehicle> Vehicles { get; set; }
        string Email { get; set; }
        string Name { get; set; }
        string PhoneNumber { get; set; }
        User.AuthLevel Authorization { get; set; }
    }
}