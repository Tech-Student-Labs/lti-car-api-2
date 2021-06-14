

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
        public async Task PostRouteReturnsStatus201_WhenUserCanBeAddedToDatabase()
        {
            //Given the User db is empty
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));

            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            await db.Database.EnsureDeletedAsync();

            var client = testServer.CreateClient();
            
            //When a list of users is requested by the api

            var registration = JsonSerializer.Serialize(new User{Id = 1, Email = "4@test.com", UserName = "harambe", Password = "monke"});
            StringContent query = new StringContent(registration, Encoding.UTF8, "application/json");

            await db.SaveChangesAsync();

            var response = await client.PostAsync("/User", query);

            // THEN the response body should return a 201 code
            response.StatusCode.Should().Be(201);

            db.Database.EnsureDeleted();
        }

        [Fact]
        public async Task PostRouteCreatesAUser_WhenUserCanBeAddedToDatabase()
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

            // THEN the database should now contain the user from the post request
            var user = db.Users.FirstOrDefault(user => user.UserName == "harambe");

            user.Should().BeEquivalentTo(monke);
            // response.Content.ReadAsStringAsync().Should().NotBeNull();

            db.Database.EnsureDeleted();
        }

        [Fact]
        public async Task PostRouteCreatesMultipleUsers_WhenAllUsersCanBeAddedToDatabase()
        {
            //Given the User db is empty
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));

            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            
            await db.Database.EnsureDeletedAsync();

            var client = testServer.CreateClient();
            
            //When a list of users is requested by the api

            const string name1 = "harambe";
            const string name2 = "amongus";

            var monke = new User{Id = 1, Email = "harambe@cincinattiZoo.org", UserName = name1, Password = "monke"};
            var among = new User{Id = 2, Email = "sus@vent.com", UserName = name2, Password = "sus"};

            var registration = JsonSerializer.Serialize(monke);
            StringContent query = new StringContent(registration, Encoding.UTF8, "application/json");

            var registration2 = JsonSerializer.Serialize(among);
            StringContent query2 = new StringContent(registration2, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User", query);
            var response2 = await client.PostAsync("/User", query2);

            // THEN the response body should return a 201 code
            var user = db.Users.FirstOrDefault(user => user.UserName == name1);
            var user2 = db.Users.FirstOrDefault(user => user.UserName == name2);

            user.Should().BeEquivalentTo(monke);
            user2.Should().BeEquivalentTo(among);

            db.Database.EnsureDeleted();
        }

        [Fact]
        public async Task ShouldReturnWhyUserWasNotAdded_WhenUsernameAlreadyUsedInDatabse()
        {
            //Given that a User already exists in the Users database
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var db = testServer.Services.GetRequiredService<DatabaseContext>(); 
            await db.Database.EnsureDeletedAsync();
            var client = testServer.CreateClient();
            db.Users.Add(new User{Email = "johndoe@test.com", UserName = "johndoe", Password = "def@123"});
            db.SaveChanges();
        
            //When a POST request is made with a username that already exists in the database
            var user = new User{Email = "johndoe2@test.com", UserName = "johndoe", Password = "whatever"};
            StringContent query = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User", query);
            var result = await response.Content.ReadAsStringAsync();
            //Then the reponse body should specify why the user was not added

            result.Should().Be("User could not be added, username already in use.");
        }

        [Fact]
        public async Task ShouldReturnCode409_WhenUsernameAlreadyUsedInDatabase()
        {
            //Given that a User already exists in the Users database
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var db = testServer.Services.GetRequiredService<DatabaseContext>(); 
            await db.Database.EnsureDeletedAsync();
            var client = testServer.CreateClient();
            db.Users.Add(new User{Email = "test@test.com", UserName = "johndoe", Password = "def@123"});
            db.SaveChanges();
        
            //When a POST request is made with a username that already exists in the database
            var user = new User{Id = 1, Email = "test2@test.com", UserName = "johndoe", Password = "whatever"};
            StringContent query = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User", query);
            //Then the reponse body should specify why the user was not added

            response.StatusCode.Should().Be(409);
        }

        [Fact]
        public async Task ShouldReturnCode409_WhenEmailAlreadyUsedInDatabase()
        {
            //Given that a User already exists in the Users database
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var db = testServer.Services.GetRequiredService<DatabaseContext>(); 
            await db.Database.EnsureDeletedAsync();
            var client = testServer.CreateClient();
            db.Users.Add(new User{UserName = "johndoe", Password = "def@123", Email = "john@doe.com"});
            db.SaveChanges();
        
            //When a POST request is made with a username that already exists in the database
            var user = new User{UserName = "robertsmith", Password = "whatever", Email = "john@doe.com"};
            StringContent query = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User", query);
            //Then the reponse body should specify why the user was not added

            response.StatusCode.Should().Be(409);
        }

        // [Fact]
        // public async Task ShouldReturnWhyUserWasNotAdded_WhenUsernameAlreadyUsedInDatabse()
        // {
        //     //Given that a User already exists in the Users database
        //     var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
        //     var db = testServer.Services.GetRequiredService<DatabaseContext>(); 
        //     await db.Database.EnsureDeletedAsync();
        //     var client = testServer.CreateClient();
        //     db.Users.Add(new User{UserName = "johndoe", Password = "def@123"});
        //     db.SaveChanges();
        
        //     //When a POST request is made with a username that already exists in the database
        //     var user = new User{UserName = "johndoe", Password = "whatever"};
        //     StringContent query = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

        //     var response = await client.PostAsync("/User", query);
        //     var result = await response.Content.ReadAsStringAsync();
        //     //Then the reponse body should specify why the user was not added

        //     result.Should().Be("User could not be added, username already in use.");
        // }

        // public async Task ShouldReturnCode409_WhenUsernameAlreadyUsedInDatabse()
        // {
        //     //Given that a User already exists in the Users database
        //     var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
        //     var db = testServer.Services.GetRequiredService<DatabaseContext>(); 
        //     await db.Database.EnsureDeletedAsync();
        //     var client = testServer.CreateClient();
        //     db.Users.Add(new User{UserName = "johndoe", Password = "def@123"});
        //     db.SaveChanges();
        
        //     //When a POST request is made with a username that already exists in the database
        //     var user = new User{Id = 1, UserName = "johndoe", Password = "whatever"};
        //     StringContent query = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

        //     var response = await client.PostAsync("/User", query);
        //     //Then the reponse body should specify why the user was not added

        //     response.StatusCode.Should().Be(409);
        // }
        
        
    }
}