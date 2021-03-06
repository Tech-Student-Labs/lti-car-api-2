using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
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
    
    private class TokenHolder{
      public string Token { get; set; }
    }

    public async Task<string> AppendJWTHeader(DatabaseContext db, HttpClient client, string username = "johndoe") {
      db.Users.Add(new User{Email = "johndoe@test.com", UserName = username, Password = "def@123"});
      db.SaveChanges();

      //handle login for JWT Token
      var user = new User{Email = "johndoe2@test.com", UserName = username, Password = "def@123"};
      StringContent query = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

      var loginResponse = await client.PostAsync("/api/auth/login", query);
      var content = await loginResponse.Content.ReadAsStringAsync();

      var result = JsonSerializer.Deserialize<TokenHolder>(content,
        new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
      
      client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.Token);

      return result.Token;
    }

    [Fact]
    public async Task should_return200CodeWhen_vehicleListIsEmpty()
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
        Year = 1997,
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
      #region 
      db.Add(new Vehicle
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
      });
      db.Add(new Vehicle
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
      });
      db.Add(new Vehicle
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
      });
      #endregion
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
    public async Task should_return20vehiclesonly()
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
        Year = 1997,
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
        Year = 1997,
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
        Year = 2005,
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
        Year = 1997,
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
        Year = 1997,
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
        Year = 2005,
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
        Year = 1997,
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
        Year = 1997,
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
        Year = 2005,
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
        Year = 1997,
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
        Year = 1997,
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
        Year = 2005,
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
        Year = 1997,
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
        Year = 1997,
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
        Year = 2005,
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
        Year = 1997,
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
        Year = 1997,
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
        Year = 2005,
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
        Year = 1997,
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
        Year = 1997,
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
        Year = 2005,
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

    [Fact]
    public async Task Should_return200WhenGetByUsernameWithNothingInDatabase()
    {
      //Given that the service is running and there are no cars in the database
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();
      await AppendJWTHeader(db, client);

      //When GET reqest is sent with a Username and rout parameter
      var response = await client.GetAsync("/Vehicle/History");
      //Then returns status 200
      response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Should_Return_An_AuthorizedUsers_Vehicles()
    {
      //Given
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();

      await AppendJWTHeader(db, client, "johndoe");

      var vehicleData = new ResponseVehicle{
        Id = 1,
        VIN = "4Y1SL65848Z411439",
        Make = "Toyota",
        Model = "Corolla",
        Year = 1997,
        Miles = 145000,
        Color = "Silver",
        SellingPrice = 2000,
        Status = Vehicle.StatusCode.Inventory,
        VehicleImages = new List<VehicleImage>(),
      };

      var user = new User{
        UserName = "johndoe",
        Email = "test@test.com",
        Id = 2,
      };

      await db.Users.AddAsync(user);

      db.SaveChanges();

      var vehicle = new Vehicle
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
        VehicleImages = new List<VehicleImage>(),
        UserId = 2,
      };

      db.Vehicles.Add(vehicle);

      db.SaveChanges();
    
      //When
      var response = await client.GetAsync("/Vehicle/History");

      //Then

      var body = JsonSerializer.Deserialize<List<ResponseVehicle>>(await response.Content.ReadAsStringAsync(),
        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

      body.Count().Should().Be(1);

      body.Should().BeEquivalentTo(vehicleData);
    }

    [Fact]
    public async Task Should_Return_An_AuthorizedUsers_Vehicles_StatusCode200()
    {
      //Given
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();

      await AppendJWTHeader(db, client, "johndoe");

      var user = new User{
        UserName = "johndoe",
        Email = "test@test.com",
        Id = 2,
      };

      await db.Users.AddAsync(user);

      db.SaveChanges();

      var vehicle = new Vehicle
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
        UserId = 2,
        User = user,
        VehicleImages = new List<VehicleImage>(),
      };

      db.Vehicles.Add(vehicle);

      db.SaveChanges();
    
      //When
      var response = await client.GetAsync("/Vehicle/History");

      //Then
      response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Should_Return_An_AuthorizedUsers_EmptyListOfVehiclesWith_StatusCode200()
    {
      //Given
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();

      await AppendJWTHeader(db, client, "johndoe");

      var user = new User{
        UserName = "johndoe",
        Email = "test@test.com",
        Id = 2,
      };

      await db.Users.AddAsync(user);

      db.SaveChanges();

      var vehicle = new Vehicle
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
        UserId = 3,
        User = user,
        VehicleImages = new List<VehicleImage>(),
      };

      db.Vehicles.Add(vehicle);

      db.SaveChanges();
    
      //When
      var response = await client.GetAsync("/Vehicle/History");

      //Then
      response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Should_Return_An_AuthorizedUsers_ListOfVehicles()
    {
      //Given
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();

      await AppendJWTHeader(db, client, "johndoe");
      var user = new User{
        UserName = "johndoe",
        Email = "test@test.com",
        Id = 2,
      };

      await db.Users.AddAsync(user);

      db.SaveChanges();

      db.Vehicles.AddRange(new Vehicle[] {
        new Vehicle {
          Id = 1,
          VIN = "4Y1SL65848Z411439",
          Make = "Toyota",
          Model = "Corolla",
          Year = 1997,
          Miles = 145000,
          Color = "Silver",
          SellingPrice = 2000,
          Status = Vehicle.StatusCode.Inventory,
          UserId = 2,
          User = user,
          VehicleImages = new List<VehicleImage>()
        },
        new Vehicle {
          Id = 2,
          VIN = "4Y1SL65848Z411439",
          Make = "Honda",
          Model = "Corolla",
          Year = 1997,
          Miles = 145000,
          Color = "Silver",
          SellingPrice = 2000,
          Status = Vehicle.StatusCode.Inventory,
          UserId = 2,
          User = user,
          VehicleImages = new List<VehicleImage>()
        },
        new Vehicle {
          Id = 3,
          VIN = "4Y1SL65848Z411439",
          Make = "Subaru",
          Model = "Corolla",
          Year = 1997,
          Miles = 145000,
          Color = "Silver",
          SellingPrice = 2000,
          Status = Vehicle.StatusCode.Inventory,
          UserId = 2,
          User = user,
          VehicleImages = new List<VehicleImage>()
        }
      });

      db.SaveChanges();
    
      //When
      var response = await client.GetAsync("/Vehicle/History");

      //Then
      var body = JsonSerializer.Deserialize<List<Vehicle>>(await response.Content.ReadAsStringAsync(),
        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

      body.Count().Should().Be(3);
    }
  }
}
