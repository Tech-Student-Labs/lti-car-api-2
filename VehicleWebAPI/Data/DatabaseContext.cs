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
          },
          new Vehicle
          {
            Id = 11,
            VIN = "5N3ZA0NE6AN906847",
            Make = "Infiniti",
            Model = "QX56",
            Year = 2010,
            Miles = 21500,
            Color = "Silver",
            SellingPrice = 15999,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 12,
            VIN = "1NXBB02E9TZ393131",
            Make = "Toyota",
            Model = "Corolla",
            Year = 1996,
            Miles = 10000,
            Color = "Black",
            SellingPrice = 15550,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 13,
            VIN = "WDBAB23A6DB369209",
            Make = "Mercedes Benz",
            Model = "C240",
            Year = 1983,
            Miles = 25000,
            Color = "Blue",
            SellingPrice = 67000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 14,
            VIN = "5J6RE4H48BL023237",
            Make = "Honda",
            Model = "CRV",
            Year = 2011,
            Miles = 22000,
            Color = "Red",
            SellingPrice = 6500,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 15,
            VIN = "JH4KA4540KC031984",
            Make = "Acura",
            Model = "Legend",
            Year = 1989,
            Miles = 84000,
            Color = "Purple",
            SellingPrice = 3200,
            Status = Vehicle.StatusCode.Sold,
            UserId = 1
          },
          new Vehicle
          {
            Id = 16,
            VIN = "1FTCR15T4GPB29162",
            Make = "Ford",
            Model = "Ranger",
            Year = 1986,
            Miles = 700000,
            Color = "Blue",
            SellingPrice = 1500,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 17,
            VIN = "5FNRL38739B001353",
            Make = "Honda",
            Model = "Odyssey",
            Year = 2009,
            Miles = 49000,
            Color = "Black",
            SellingPrice = 11200,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 18,
            VIN = "KMHD25LE1DU042025",
            Make = "Hyundai",
            Model = "Elantra",
            Year = 2013,
            Miles = 62335,
            Color = "White",
            SellingPrice = 15000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 19,
            VIN = "JN8AS1MU0CM120061",
            Make = "Infiniti",
            Model = "Fx35",
            Year = 2012,
            Miles = 98700,
            Color = "Beige",
            SellingPrice = 35000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 20,
            VIN = "1J4FT68SXXL633294",
            Make = "Jeep",
            Model = "Cherokee",
            Year = 1999,
            Miles = 42000,
            Color = "White",
            SellingPrice = 52000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 21,
            VIN = "WMWZN3C51BT133317",
            Make = "Mini",
            Model = "Cooper",
            Year = 2011,
            Miles = 145000,
            Color = "Silver",
            SellingPrice = 16000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 22,
            VIN = "JH4NA1150PT000087",
            Make = "Acura",
            Model = "NSX",
            Year = 1993,
            Miles = 16514,
            Color = "Black",
            SellingPrice = 23186,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 23,
            VIN = "1GKEL19WXRB546238",
            Make = "GMC",
            Model = "Safari",
            Year = 1994,
            Miles = 984132,
            Color = "Blue",
            SellingPrice = 4549,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 24,
            VIN = "JM1BF2325G0V37585",
            Make = "Mazda",
            Model = "323",
            Year = 1986,
            Miles = 222000,
            Color = "Red",
            SellingPrice = 6511,
            Status = Vehicle.StatusCode.Pending,
            UserId = 1
          },
          new Vehicle
          {
            Id = 25,
            VIN = "4T1BF3EK5BU638805",
            Make = "Toyota",
            Model = "Camry",
            Year = 2011,
            Miles = 98461,
            Color = "Purple",
            SellingPrice = 54166,
            Status = Vehicle.StatusCode.Sold,
            UserId = 1
          },
          new Vehicle
          {
            Id = 26,
            VIN = "1G2JB12F047226515",
            Make = "Pontiac",
            Model = "Sunfire",
            Year = 2004,
            Miles = 96841,
            Color = "Blue",
            SellingPrice = 36545,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 27,
            VIN = "5XYZUDLB7DG006717",
            Make = "Hyundai",
            Model = "Santa Fe Sport",
            Year = 2013,
            Miles = 46333,
            Color = "Black",
            SellingPrice = 54631,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 28,
            VIN = "1D7HU18D54S747050",
            Make = "Dodge",
            Model = "Ram Pickup 1500",
            Year = 2004,
            Miles = 345531,
            Color = "White",
            SellingPrice = 16512,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 29,
            VIN = "SAJDA41GXDPA26883",
            Make = "Jaguar",
            Model = "S-Type",
            Year = 1983,
            Miles = 15000,
            Color = "Beige",
            SellingPrice = 2000000,
            Status = Vehicle.StatusCode.Inventory,
            UserId = 1
          },
          new Vehicle
          {
            Id = 30,
            VIN = "ZFFEW59A190165924",
            Make = "Ferrari",
            Model = "F430",
            Year = 2009,
            Miles = 5600,
            Color = "White",
            SellingPrice = 500000,
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
          new InventoryVehicle { Id = 8, VehicleId = 10, Price = 14200.00 },
          new InventoryVehicle { Id = 9, VehicleId = 11, Price = 15999.00 },
          new InventoryVehicle { Id = 10, VehicleId = 12, Price = 15550.00 },
          new InventoryVehicle { Id = 11, VehicleId = 13, Price = 67000.00 },
          new InventoryVehicle { Id = 12, VehicleId = 14, Price = 6500.00 },
          new InventoryVehicle { Id = 13, VehicleId = 16, Price = 1500.00 },
          new InventoryVehicle { Id = 14, VehicleId = 17, Price = 11200.00 },
          new InventoryVehicle { Id = 15, VehicleId = 18, Price = 15000.00 },
          new InventoryVehicle { Id = 16, VehicleId = 19, Price = 35000.00 },
          new InventoryVehicle { Id = 17, VehicleId = 20, Price = 52000.00 },
          new InventoryVehicle { Id = 18, VehicleId = 21, Price = 16000.00 },
          new InventoryVehicle { Id = 19, VehicleId = 22, Price = 23186.00 },
          new InventoryVehicle { Id = 20, VehicleId = 23, Price = 4549.00 },
          new InventoryVehicle { Id = 21, VehicleId = 26, Price = 36545.00 },
          new InventoryVehicle { Id = 22, VehicleId = 27, Price = 54631.00 },
          new InventoryVehicle { Id = 23, VehicleId = 28, Price = 16512.00 },
          new InventoryVehicle { Id = 24, VehicleId = 29, Price = 2000000.00 },
          new InventoryVehicle { Id = 25, VehicleId = 30, Price = 500000.00 }

      );

      // modelBuilder.Entity<VehicleImage>().HasNoKey();
      modelBuilder.Entity<VehicleImage>().HasData(
          new VehicleImage { Id = 1, VehicleId = 1 }
      );

      base.OnModelCreating(modelBuilder);
    }
  }
}