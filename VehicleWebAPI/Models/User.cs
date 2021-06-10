using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VehicleWebAPI.Interfaces;

namespace VehicleWebAPI.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [DefaultValue(AuthLevel.Guest)]
        public AuthLevel Authorization { get; set; }

        public enum AuthLevel
        {
            Guest,
            User,
            Admin
        }
    }
}