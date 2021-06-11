

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
using VehicleWebAPI.Models;
using VehicleWebAPI;
using Xunit;
using System.Net.Http;
using System.Text;

namespace VehicleTests.E2E_Tests
{

    public class UserPostRoute
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
        public async Task test2323()
        {
            //Given the User db is empty
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));

            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            db.Database.EnsureDeleted();

            var client = testServer.CreateClient();
            
            //When a list of users is requested by the api

            var registration = JsonSerializer.Serialize(new User{UserName = "johndoe", Password = "def@123"});
            StringContent query = new StringContent(registration, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User", query);

            db.SaveChanges();

            // THEN the response body should return a 200 code
            response.StatusCode.Should().Be(200);

            db.Database.EnsureDeleted();
            
            
            // var response = await client.GetAsync("/User");
            // var content = await response.Content.ReadAsStringAsync();
            // var result = JsonSerializer.Deserialize<List<object>>(content);

            //Then return an empty list
            // result.Count.Should().Be(0);
        }
        
        
    }
}