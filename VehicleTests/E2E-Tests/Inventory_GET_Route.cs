using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleDatabase.Data;
using VehicleWebAPI;
using Xunit;

namespace VehicleTests.E2E_Tests{

    public class InventoryGetRoute{

        private IWebHostBuilder hostBuilder(string guid)
        {
            return new WebHostBuilder()
                      .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location))
                      .UseStartup<Startup>()
                      .ConfigureServices(services =>
                      {
                          services.Remove(services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<DatabaseContext>)));
                          services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("vehicle" + guid));
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
    }
}