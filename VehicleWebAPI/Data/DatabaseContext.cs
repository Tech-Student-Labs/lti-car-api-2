using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VehicleWebAPI.Models;

namespace VehicleDatabase.Data
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext() : base() { }
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
            VIN = "4Y1SL65848Z411439",
            Make = "Toyota",
            Model = "Corolla",
            Year = 1997,
            Miles = 145000,
            Color = "Silver",
            SellingPrice = 2000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 2,
            VIN = "5Z1SL39746U411411",
            Make = "Honda",
            Model = "Civic",
            Year = 1997,
            Miles = 145000,
            Color = "Black",
            SellingPrice = 3000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 3,
            VIN = "7T1SL646726411440",
            Make = "Subaru",
            Model = "Impreza",
            Year = 2005,
            Miles = 175000,
            Color = "Blue",
            SellingPrice = 4000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 4,
            VIN = "9P1SL658486268352",
            Make = "Mazda",
            Model = "3",
            Year = 2007,
            Miles = 200000,
            Color = "Red",
            SellingPrice = 2000,
            Status = Vehicle.StatusCode.Pending,
            UserId = 1
          },
          new Vehicle
          {
            Id = 5,
            VIN = "YU1SL658486123463",
            Make = "Mitsubishi",
            Model = "Eclipse",
            Year = 2005,
            Miles = 75000,
            Color = "Purple",
            SellingPrice = 6000,
            Status = Vehicle.StatusCode.Sold,
            UserId = 1
          },
          new Vehicle
          {
            Id = 6,
            VIN = "WVWAA71K08W201030",
            Make = "Volkswagen",
            Model = "Rabbit",
            Year = 2015,
            Miles = 15600,
            Color = "Blue",
            SellingPrice = 35000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 7,
            VIN = "2FTHF25H6LCB36173",
            Make = "Ford",
            Model = "F250",
            Year = 1990,
            Miles = 154000,
            Color = "Black",
            SellingPrice = 7400,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 8,
            VIN = "JH4DA3341JS014654",
            Make = "Acura",
            Model = "Integra",
            Year = 1988,
            Miles = 352000,
            Color = "White",
            SellingPrice = 2000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 9,
            VIN = "5J6RM4H75CL059384",
            Make = "Honda",
            Model = "CRV",
            Year = 2012,
            Miles = 98000,
            Color = "Beige",
            SellingPrice = 7800,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 10,
            VIN = "JNKCV64E78M131002",
            Make = "Infiniti",
            Model = "G37",
            Year = 2008,
            Miles = 42000,
            Color = "White",
            SellingPrice = 14200,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          }
      );

      modelBuilder.Entity<User>().HasData(
          new User { Id = 1, Authorization = User.AuthLevel.User, Name = "Robert Muehler" }
      );

      // modelBuilder.Entity<InventoryVehicle>().HasNoKey();
      modelBuilder.Entity<InventoryVehicle>().HasData(
          new InventoryVehicle { Id = 1, VehicleId = 1, Price = 6000.00 },
          new InventoryVehicle { Id = 2, VehicleId = 2, Price = 21600.00 },
          new InventoryVehicle { Id = 3, VehicleId = 3, Price = 35400.00 },
          new InventoryVehicle { Id = 4, VehicleId = 6, Price = 35000.00 },
          new InventoryVehicle { Id = 5, VehicleId = 7, Price = 7400.00 },
          new InventoryVehicle { Id = 6, VehicleId = 8, Price = 2000.00 },
          new InventoryVehicle { Id = 7, VehicleId = 9, Price = 7800.00 },
          new InventoryVehicle { Id = 8, VehicleId = 10, Price = 14200.00 }

      );

      // modelBuilder.Entity<VehicleImage>().HasNoKey();
      modelBuilder.Entity<VehicleImage>().HasData(
          new VehicleImage { Id = 1, VehicleId = 1 }
      );

      base.OnModelCreating(modelBuilder);
    }
  }
}