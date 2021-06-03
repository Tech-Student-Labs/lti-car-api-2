using System.Collections.Generic;
using Models;
using VehicleDatabase.Interfaces;

namespace VehicleDatabase.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public AuthLevel Authorization { get; set; }

        public enum AuthLevel
        {
            Guest,
            User,
            Admin
        }
    }
}