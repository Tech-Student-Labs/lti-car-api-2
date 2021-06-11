using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
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
  public class Vehicle_PUT_Route
  {
    private IWebHostBuilder hostBuilder => new WebHostBuilder()
        .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location))
        .UseStartup<Startup>()
        .ConfigureServices(services =>
        {
          services.Remove(
                  services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<DatabaseContext>)));
          services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("VehiclePost"));
        });

    [Fact]
    public async Task Should_ReturnNull_IfNoVehicleToUpdateExists()
    {
      //Given the service is running and there are no vehicles in db
      var testServer = new TestServer(hostBuilder);
      var client = testServer.CreateClient();
      var db = testServer.Services.GetRequiredService<DatabaseContext>();
      db.Database.EnsureDeleted();
      var vehicleToUpdate = JsonSerializer.Serialize(new Vehicle
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
      //When User attempts to update a vehicle
      var response = await client.PutAsJsonAsync("/vehicle", vehicleToUpdate);

      //Then the response should be invalid
      response.StatusCode.Should().Be(415);
    }
  }
}