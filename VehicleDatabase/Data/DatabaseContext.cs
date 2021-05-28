using Microsoft.EntityFrameworkCore;
using Models;

namespace VehicleDatabase.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Vehicle.db");
            base.OnConfiguring(optionsBuilder);
        }

        /*
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public int Miles { get; set; }
        public string Color { get; set; }
        public string[] Images { get; set; }
        public double sellingPrice { get; set; }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { VIN = "4Y1SL65848Z411439", Make = "Toyota", Model = "Corolla", Year = "1997", Miles = 145000, Color = "Silver", sellingPrice = 2000, ImageURI = "https://via.placeholder.com/150" },
                new Vehicle { VIN = "5Z1SL65848A411439", Make = "Honda", Model = "Civic", Year = "1997", Miles = 145000, Color = "Black", sellingPrice = 3000, ImageURI = "https://via.placeholder.com/150" },
                new Vehicle { VIN = "7T1SL658486411439", Make = "Subaru", Model = "Impreza", Year = "2005", Miles = 175000, Color = "Blue", sellingPrice = 4000, ImageURI = "https://via.placeholder.com/150"}
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}