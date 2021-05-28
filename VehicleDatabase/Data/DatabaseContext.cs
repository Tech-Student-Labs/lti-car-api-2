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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { VIN = "4Y1SL65848Z411439", Make = "Toyota", Model = "Corolla", Year = "1997", Miles = 145000, Color = "Silver", sellingPrice = 2000, ImageURI = "https://via.placeholder.com/150" },
                new Vehicle { VIN = "5Z1SL39746U411411", Make = "Honda", Model = "Civic", Year = "1997", Miles = 145000, Color = "Black", sellingPrice = 3000, ImageURI = "https://via.placeholder.com/150" },
                new Vehicle { VIN = "7T1SL646726411440", Make = "Subaru", Model = "Impreza", Year = "2005", Miles = 175000, Color = "Blue", sellingPrice = 4000, ImageURI = "https://via.placeholder.com/150"},
                new Vehicle { VIN = "9P1SL658486268352", Make = "Mazda", Model = "3", Year = "2007", Miles = 200000, Color = "Red", sellingPrice = 2000, ImageURI = "https://via.placeholder.com/150"},
                new Vehicle { VIN = "YU1SL658486123463", Make = "Mitsubishi", Model = "Eclipse", Year = "2005", Miles = 75000, Color = "Purple", sellingPrice = 6000, ImageURI = "https://via.placeholder.com/150"},
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}