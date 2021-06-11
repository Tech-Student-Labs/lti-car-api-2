

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
        public async Task PostRouteReturnsStatus200()
        {
            //Given the User db is empty
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));

            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            db.Database.EnsureDeleted();

            var client = testServer.CreateClient();
            
            //When a list of users is requested by the api

            var registration = JsonSerializer.Serialize(new User{UserName = "harambe", Password = "monke"});
            StringContent query = new StringContent(registration, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User", query);

            db.SaveChanges();

            // THEN the response body should return a 200 code
            response.StatusCode.Should().Be(201);

            db.Database.EnsureDeleted();
        }

        [Fact]
        public async Task PostRouteCreatesAUser()
        {
            //Given the User db is empty
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));

            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            db.Database.EnsureDeleted();

            var client = testServer.CreateClient();
            
            //When a list of users is requested by the api

            var monke = new User{Id = 1, UserName = "harambe", Password = "monke"};

            var registration = JsonSerializer.Serialize(monke);
            StringContent query = new StringContent(registration, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User", query);

            db.SaveChanges();

            // THEN the response body should return a 200 code
            var user = db.Users.FirstOrDefault(user => user.UserName == "harambe");

            user.Should().BeEquivalentTo(monke);

            db.Database.EnsureDeleted();
        }

        [Fact]
        public async Task PostRouteCreatesMultipleUsers()
        {
            //Given the User db is empty
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));

            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            
            await db.Database.EnsureDeletedAsync();

            var client = testServer.CreateClient();
            
            //When a list of users is requested by the api

            const string name1 = "harambe";
            const string name2 = "amongus";

            var monke = new User{Id = 1, UserName = name1, Password = "monke"};
            var among = new User{Id = 2, UserName = name2, Password = "sus"};

            var registration = JsonSerializer.Serialize(monke);
            StringContent query = new StringContent(registration, Encoding.UTF8, "application/json");

            var registration2 = JsonSerializer.Serialize(among);
            StringContent query2 = new StringContent(registration2, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User", query);
            var response2 = await client.PostAsync("/User", query2);

            db.SaveChanges();

            // THEN the response body should return a 200 code
            var user = db.Users.FirstOrDefault(user => user.UserName == name1);
            var user2 = db.Users.FirstOrDefault(user => user.UserName == name2);

            user.Should().BeEquivalentTo(monke);
            user2.Should().BeEquivalentTo(among);

            db.Database.EnsureDeleted();
        }
        
        
    }
}