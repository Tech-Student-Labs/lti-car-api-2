using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleWebAPI.Interfaces;

namespace VehicleWebAPI.Models
{
    public class Vehicle : ResponseVehicle, IVehicle
    {
        public User User { get; set; }
        public int UserId { get; set; }
    }
}