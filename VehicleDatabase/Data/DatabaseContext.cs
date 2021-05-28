using Microsoft.EntityFrameworkCore;
using Models;

namespace VehicleDatabase.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<InventoryVehicle> Inventory { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Vehicle.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    VIN = "4Y1SL65848Z411439", Make = "Toyota", Model = "Corolla", Year = "1997", Miles = 145000,
                    Color = "Silver", SellingPrice = 2000, Status = Vehicle.StatusCode.Inventory
                },
                new Vehicle
                {
                    VIN = "5Z1SL39746U411411", Make = "Honda", Model = "Civic", Year = "1997", Miles = 145000,
                    Color = "Black", SellingPrice = 3000, Status = Vehicle.StatusCode.Inventory
                },
                new Vehicle
                {
                    VIN = "7T1SL646726411440", Make = "Subaru", Model = "Impreza", Year = "2005", Miles = 175000,
                    Color = "Blue", SellingPrice = 4000, Status = Vehicle.StatusCode.Inventory
                },
                new Vehicle
                {
                    VIN = "9P1SL658486268352", Make = "Mazda", Model = "3", Year = "2007", Miles = 200000,
                    Color = "Red", SellingPrice = 2000, Status = Vehicle.StatusCode.Pending
                },
                new Vehicle
                {
                    VIN = "YU1SL658486123463", Make = "Mitsubishi", Model = "Eclipse", Year = "2005", Miles = 75000,
                    Color = "Purple", SellingPrice = 6000, Status = Vehicle.StatusCode.Sold
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User()
            );

            modelBuilder.Entity<InventoryVehicle>().HasData(
                new InventoryVehicle()
            );

            modelBuilder.Entity<VehicleImage>().HasData(
                new VehicleImage()
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}