using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleWebAPI.Interfaces;

namespace VehicleWebAPI.Models
{
    public class Vehicle : IVehicle
    {
        public int Id { get; set; }
        [Required]
        public string VIN { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public int Miles { get; set; }
        [Required]
        public string Color { get; set; }
        
        public List<VehicleImage> VehicleImages { get; set; }
        [Required]
        public double SellingPrice { get; set; }
        
        public User User { get; set; }
        [Required]
        public int UserId { get; set; }
        [DefaultValue(StatusCode.Pending)]
        public StatusCode Status { get; set; }

        public enum StatusCode
        {
            Pending,
            Inventory,
            Sold
        }
    }
}