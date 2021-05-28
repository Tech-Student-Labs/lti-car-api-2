using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Vehicle : IVehicle
    {
        [Key] 
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public int Miles { get; set; }
        public string Color { get; set; }
        public string ImageURI { get; set; }
        public double sellingPrice { get; set; }
    }
}