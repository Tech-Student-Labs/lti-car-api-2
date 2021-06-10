using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehicleDatabase.Data;
using VehicleWebAPI;
using VehicleWebAPI.Models;
using Xunit;


namespace VehicleTests.E2E_Tests
{
  public class Vehicles_GET_Route
  {

    private IWebHostBuilder hostBuilder => new WebHostBuilder()
      .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location))
      .UseStartup<Startup>()
      .ConfigureServices(services =>
      {
        services.Remove(services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<DatabaseContext>)));
        services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("vehicle"));
      });

    [Fact]
    public async Task TestName()
    {
      //GIVEN that the service is running and the vehicle list is empty
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();

      //WHEN GET is called
      var result = await client.GetAsync("/Vehicle");

      //THEN return 200 
      result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Should_ReturnAnEmptyListWhen_GETisCalled()
    {
      //Given that the service is running and the vehicle list is empty
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();
      //When GET is called
      var response = await client.GetAsync("/Vehicle");
      var content = await response.Content.ReadAsStringAsync();
      var result = JsonSerializer.Deserialize<List<object>>(content);
      //Then return an empty list
      result.Count().Should().Be(0);
    }

    [Fact]
    public async Task Should_return200WhenThereIsOneCarInTheDatabaseand_GETisCalled()
    {
      //GIVEN that the service is running and there is one vehicle in the database
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();
      Vehicle car = new Vehicle
      {
        Id = 1,
        VIN = "4Y1SL65848Z411439",
        Make = "Toyota",
        Model = "Corolla",
        Year = "1997",
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1,
        VehicleImages = new List<VehicleImage>()
      };
      db.Add(car);
      db.SaveChanges();
      //WHEN GET is called
      var result = await client.GetAsync("/Vehicle");
      var body = JsonSerializer.Deserialize<List<Vehicle>>(await result.Content.ReadAsStringAsync(),
      new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

      //THEN there should be to return 
      body.Should().BeEquivalentTo(car);
      db.Database.EnsureDeleted();
    }

    [Fact]
    public async Task Should_Return1()
    {
      //GIVEN that the service is running and there are many vehicles in the database
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();
      db.Add(new Vehicle
      {
        Id = 1,
        VIN = "4Y1SL65848Z411439",
        Make = "Toyota",
        Model = "Corolla",
        Year = "1997",
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 2,
        VIN = "5Z1SL39746U411411",
        Make = "Honda",
        Model = "Civic",
        Year = "1997",
        Miles = 145000,
        Color = "Black",
        SellingPrice = 3000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 3,
        VIN = "7T1SL646726411440",
        Make = "Subaru",
        Model = "Impreza",
        Year = "2005",
        Miles = 175000,
        Color = "Blue",
        SellingPrice = 4000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });

      db.SaveChanges();
      //WHEN GET is called
      var result = await client.GetAsync("/Vehicle");
      var body = JsonSerializer.Deserialize<List<Vehicle>>(await result.Content.ReadAsStringAsync(),
      new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

      //THEN returns 1 vehicle 
      body.Count().Should().Be(3);
      db.Database.EnsureDeleted();
    }

    [Fact]
    public async Task should_return20()
    {
      //Given That the service is running and there are more than 20 cars in the database
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();
      //Region for 21 add vehicles
      #region 
      db.Add(new Vehicle
      {
        Id = 1,
        VIN = "4Y1SL65848Z411439",
        Make = "Toyota",
        Model = "Corolla",
        Year = "1997",
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 2,
        VIN = "5Z1SL39746U411411",
        Make = "Honda",
        Model = "Civic",
        Year = "1997",
        Miles = 145000,
        Color = "Black",
        SellingPrice = 3000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 3,
        VIN = "7T1SL646726411440",
        Make = "Subaru",
        Model = "Impreza",
        Year = "2005",
        Miles = 175000,
        Color = "Blue",
        SellingPrice = 4000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 4,
        VIN = "4Y1SL65848Z411439",
        Make = "Toyota",
        Model = "Corolla",
        Year = "1997",
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 5,
        VIN = "5Z1SL39746U411411",
        Make = "Honda",
        Model = "Civic",
        Year = "1997",
        Miles = 145000,
        Color = "Black",
        SellingPrice = 3000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 6,
        VIN = "7T1SL646726411440",
        Make = "Subaru",
        Model = "Impreza",
        Year = "2005",
        Miles = 175000,
        Color = "Blue",
        SellingPrice = 4000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 7,
        VIN = "4Y1SL65848Z411439",
        Make = "Toyota",
        Model = "Corolla",
        Year = "1997",
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 8,
        VIN = "5Z1SL39746U411411",
        Make = "Honda",
        Model = "Civic",
        Year = "1997",
        Miles = 145000,
        Color = "Black",
        SellingPrice = 3000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 9,
        VIN = "7T1SL646726411440",
        Make = "Subaru",
        Model = "Impreza",
        Year = "2005",
        Miles = 175000,
        Color = "Blue",
        SellingPrice = 4000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 10,
        VIN = "4Y1SL65848Z411439",
        Make = "Toyota",
        Model = "Corolla",
        Year = "1997",
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 11,
        VIN = "5Z1SL39746U411411",
        Make = "Honda",
        Model = "Civic",
        Year = "1997",
        Miles = 145000,
        Color = "Black",
        SellingPrice = 3000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 12,
        VIN = "7T1SL646726411440",
        Make = "Subaru",
        Model = "Impreza",
        Year = "2005",
        Miles = 175000,
        Color = "Blue",
        SellingPrice = 4000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 13,
        VIN = "4Y1SL65848Z411439",
        Make = "Toyota",
        Model = "Corolla",
        Year = "1997",
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 14,
        VIN = "5Z1SL39746U411411",
        Make = "Honda",
        Model = "Civic",
        Year = "1997",
        Miles = 145000,
        Color = "Black",
        SellingPrice = 3000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 15,
        VIN = "7T1SL646726411440",
        Make = "Subaru",
        Model = "Impreza",
        Year = "2005",
        Miles = 175000,
        Color = "Blue",
        SellingPrice = 4000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 16,
        VIN = "4Y1SL65848Z411439",
        Make = "Toyota",
        Model = "Corolla",
        Year = "1997",
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 17,
        VIN = "5Z1SL39746U411411",
        Make = "Honda",
        Model = "Civic",
        Year = "1997",
        Miles = 145000,
        Color = "Black",
        SellingPrice = 3000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 18,
        VIN = "7T1SL646726411400",
        Make = "Subaru",
        Model = "Impreza",
        Year = "2005",
        Miles = 175000,
        Color = "Blue",
        SellingPrice = 4000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 19,
        VIN = "4Y1SL65848Z411430",
        Make = "Toyota",
        Model = "Corolla",
        Year = "1997",
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 20,
        VIN = "5Z1SL39746U411410",
        Make = "Honda",
        Model = "Civic",
        Year = "1997",
        Miles = 145000,
        Color = "Black",
        SellingPrice = 3000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      db.Add(new Vehicle
      {
        Id = 21,
        VIN = "7T1SL64672641144",
        Make = "Subaru",
        Model = "Impreza",
        Year = "2005",
        Miles = 175000,
        Color = "Blue",
        SellingPrice = 4000,
        Status = Vehicle.StatusCode.Inventory,
        UserId = 1
      });
      #endregion
      db.SaveChanges();
      //When GET is called
      var result = await client.GetAsync("/Vehicle");
      var body = JsonSerializer.Deserialize<List<Vehicle>>(await result.Content.ReadAsStringAsync(),
      new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
      //Then return only 20 vehicles
      body.Count().Should().Be(20);
      db.Database.EnsureDeleted();
    }
  }
}