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
        //When GET is called
        var response = await client.GetAsync("/Vehicle");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<object>>(content);
        //Then return an empty list
        result.Count().Should().Be(0);
        }

        // [Fact]
        // public async Task Should_return200WhenThereIsOneCarInTheDatabaseand_GETisCalled()
        // {
        // //GIVEN that the service is running and there is one vehicle in the database
        // var testServer = new TestServer(hostBuilder);
        // var client = testServer.CreateClient();
        // var db = testServer.Services.GetRequiredService<DatabaseContext>();
        // db.Database.EnsureDeleted();
        // db.Database.EnsureCreated();
        // db.Add(new Vehicle
        //         {
        //             Id = 1,
        //             VIN = "4Y1SL65848Z411439", Make = "Toyota", Model = "Corolla", Year = "1997", Miles = 145000,
        //             Color = "Silver", SellingPrice = 2000, Status = Vehicle.StatusCode.Inventory, UserId = 1
        //         });
        // db.SaveChanges();
        // //WHEN GET is called
        // var result = await client.GetAsync("/Vehicle");
        // var body = JsonSerializer.Deserialize<List<object>>(await result.Content.ReadAsStringAsync());

        // //THEN return 200 
        // body.Count().Should().Be(1);
        // db.Database.EnsureDeleted();
        // }
    }
}