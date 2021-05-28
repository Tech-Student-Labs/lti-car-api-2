using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Vehicle : IVehicle
    {
        [Key] public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public int Miles { get; set; }
        public string Color { get; set; }
        public List<VehicleImage> Images { get; set; }
        public double SellingPrice { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public StatusCode Status { get; set; }

        public enum StatusCode
        {
            Pending,
            Inventory,
            Sold
        }
    }
}