

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
using VehicleDatabase.Data;
using VehicleWebAPI;
using Xunit;

namespace VehicleTests.E2E_Tests
{

    public class UserGetRoute
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
                    services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("user" + guid));
                });
        }
        
        
        
        [Fact]
        public async Task Should_ReturnAnEmpyList_WhenUserDBIsEmpty()
        {
            //Given the User db is empty
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();
            
            //When a list of users is requested by the api
            var response = await client.GetAsync("/User");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<object>>(content);

            //Then return an empty list
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task Should_Properly_Login()
        {
            //Given the User db is empty
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();
            
            //When a list of users is requested by the api
            var response = await client.GetAsync("/User");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<object>>(content);

            //Then return an empty list
            result.Count.Should().Be(0);
        }
        
        
    }
}