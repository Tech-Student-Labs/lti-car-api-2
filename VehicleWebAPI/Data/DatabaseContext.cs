using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VehicleWebAPI.Models;

namespace VehicleDatabase.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() {}
        public DatabaseContext(DbContextOptions options) : base(options) 
        {
            
        }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public virtual DbSet<InventoryVehicle> Inventory { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            //optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Vehicle;User=sa;Password=SusPassword2!");
            // optionsBuilder.UseSqlite("Data Source=Vehicle.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    Id = 1,
                    VIN = "4Y1SL65848Z411439", Make = "Toyota", Model = "Corolla", Year = "1997", Miles = 145000,
                    Color = "Silver", SellingPrice = 2000, Status = Vehicle.StatusCode.Inventory, UserId = 1
                },
                new Vehicle
                {
                    Id = 2,
                    VIN = "5Z1SL39746U411411", Make = "Honda", Model = "Civic", Year = "1997", Miles = 145000,
                    Color = "Black", SellingPrice = 3000, Status = Vehicle.StatusCode.Inventory, UserId = 1
                },
                new Vehicle
                {
                    Id = 3,
                    VIN = "7T1SL646726411440", Make = "Subaru", Model = "Impreza", Year = "2005", Miles = 175000,
                    Color = "Blue", SellingPrice = 4000, Status = Vehicle.StatusCode.Inventory, UserId = 1
                },
                new Vehicle
                {
                    Id = 4,
                    VIN = "9P1SL658486268352", Make = "Mazda", Model = "3", Year = "2007", Miles = 200000,
                    Color = "Red", SellingPrice = 2000, Status = Vehicle.StatusCode.Pending, UserId = 1
                },
                new Vehicle
                {
                    Id = 5,
                    VIN = "YU1SL658486123463", Make = "Mitsubishi", Model = "Eclipse", Year = "2005", Miles = 75000,
                    Color = "Purple", SellingPrice = 6000, Status = Vehicle.StatusCode.Sold, UserId = 1
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User {Id = 1, Authorization = User.AuthLevel.User, Name = "Robert Muehler"}
            );
            
            // modelBuilder.Entity<InventoryVehicle>().HasNoKey();
            modelBuilder.Entity<InventoryVehicle>().HasData(
                new InventoryVehicle {Id = 1, VehicleId = 1, Price = 6000.00}
            );

            // modelBuilder.Entity<VehicleImage>().HasNoKey();
            modelBuilder.Entity<VehicleImage>().HasData(
                new VehicleImage {Id = 1, VehicleId = 1}
            );
        
            base.OnModelCreating(modelBuilder);
        }
    }
}