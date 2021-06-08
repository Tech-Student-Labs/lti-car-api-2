using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
  public class Vehicle_POST_Route
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
    public async Task Should_Return201_WhenPostRequestIsSubmitted()
    {
      // GIVEN the service is running and there are no items in todo list
      var testServer = new TestServer(hostBuilder);
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();

      var client = testServer.CreateClient();
      var clean = JsonSerializer.Serialize(new Vehicle
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
      StringContent query = new StringContent(clean, Encoding.UTF8, "application/json");

      // WHEN a POST request is submitted to the api/ToDo 
      var result = await client.PostAsync("/Vehicle", query);
      db.SaveChanges();

      // THEN the response body should return a 201 code
      result.StatusCode.Should().Be(201);
      db.Database.EnsureDeleted();
    }


    [Fact]
    public async Task Should_CreateVehicleInDB_WhenPostRequestIsSubmitted()
    {
      // GIVEN the service is running and there are no items in todo list
      var testServer = new TestServer(hostBuilder);
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();

      var client = testServer.CreateClient();
      var clean = JsonSerializer.Serialize(new Vehicle
      {
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
      StringContent query = new StringContent(clean, Encoding.UTF8, "application/json");

      // WHEN a POST request is submitted to the api/ToDo
      var result = await client.PostAsync("/Vehicle", query);
      db.SaveChanges();

      // THEN the response body should return the path to the GET of the ToDo
      var body = JsonSerializer.Deserialize<Vehicle>(await result.Content.ReadAsStringAsync());
      body.VIN.Should().Be("4Y1SL65848Z411439");
      db.Database.EnsureDeleted();
    }

    [Fact]
    public async Task Should_Return201_WhenCreated_AndThereAlreadyExists1Todo()
    {
      //   GIVEN the service is running and there is 1 item in todo list
      var testServer = new TestServer(hostBuilder);
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();

      var client = testServer.CreateClient();
      var clean = JsonSerializer.Serialize(new Vehicle
      {
        VIN = "YU1SL658486123463",
        Make = "Mitsubishi",
        Model = "Eclipse",
        Year = "2005",
        Miles = 75000,
        Color = "Purple",
        SellingPrice = 6000,
        Status = Vehicle.StatusCode.Sold,
        UserId = 1
      });
      StringContent query = new StringContent(clean, Encoding.UTF8, "application/json");
      db.Add(new Vehicle
      {
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
      // WHEN a POST request is submitted to the api/ToDo 
      var result = await client.PostAsync("/api/ToDo", query);
      db.SaveChanges();

      // THEN the response body should return a 201 code
      result.StatusCode.Should().Be(201);
      db.Database.EnsureDeleted();
    }

    [Fact]
    public async Task Should_CreateVehicleInDB_WhenPostIsSubmittedAnd1TodoExists()
    {
      //   GIVEN the service is running and there is 1 item in todo list
      var testServer = new TestServer(hostBuilder);
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();

      var client = testServer.CreateClient();
      var clean = JsonSerializer.Serialize(new Vehicle
      {
        VIN = "YU1SL658486123463",
        Make = "Mitsubishi",
        Model = "Eclipse",
        Year = "2005",
        Miles = 75000,
        Color = "Purple",
        SellingPrice = 6000,
        Status = Vehicle.StatusCode.Sold,
        UserId = 1
      });
      StringContent query = new StringContent(clean, Encoding.UTF8, "application/json");
      db.Add(new Vehicle
      {
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

      // WHEN a POST request is submitted to the api/ToDo 
      var result = await client.PostAsync("/api/ToDo", query);
      db.SaveChanges();

      //   THEN the response body should return the path to the GET of the ToDo
      var body = JsonSerializer.Deserialize<Vehicle>(await result.Content.ReadAsStringAsync());
      body.VIN.Should().Be("4Y1SL65848Z411439");
      db.Database.EnsureDeleted();
    }


  }
}