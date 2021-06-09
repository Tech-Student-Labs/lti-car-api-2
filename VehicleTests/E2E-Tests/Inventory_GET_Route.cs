using System;
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
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using VehicleDatabase.Data;
using VehicleWebAPI;
using VehicleWebAPI.Models;
using Xunit;

namespace VehicleTests.E2E_Tests
{
    public class InventoryGetRoute
    {
        private IWebHostBuilder hostBuilder(string guid)
        {
            return new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location))
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.Remove(services.SingleOrDefault(s =>
                        s.ServiceType == typeof(DbContextOptions<DatabaseContext>)));
                    services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("inventory" + guid));
                });
        }

        [Fact]
        public async Task Should_ReturnStatusCode404_WhenInventoryVehicleNotFound()
        {
            //Given
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();

            //WHEN GET is called
            var result = await client.GetAsync("/Inventory");

            //THEN return 404
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Should_ReturnStatusCode200IfDBIsEmpty_WhenListVehiclesIsCalled()
        {
            //Given
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();

            // WHEN
            var response = await client.GetAsync("/Inventory");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<object>>(content);
            //THEN return 404

            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task Should_ReturnInventoryVehicle_WhenVehicleIdExistsInDB()
        {
            //Given
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            var vehicle = new Vehicle
            {
                Id = 1,
                VIN = "4Y1SL65848Z411439", Make = "Toyota", Model = "Corolla", Year = "1997", Miles = 145000,
                Color = "Silver", SellingPrice = 2000, Status = Vehicle.StatusCode.Inventory, UserId = 1
            };
            var inventoryVehicle = new InventoryVehicle {Id = 1, VehicleId = 1, Price = 3000, Vehicle = vehicle};
            db.Add(inventoryVehicle);
            db.SaveChanges();

            // WHEN
            var response = await client.GetAsync("/Inventory/1");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<InventoryVehicle>(content,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            
            //THEN inventoryVehicle should be returned
            result.Should().BeEquivalentTo(inventoryVehicle);
        }
    }
}