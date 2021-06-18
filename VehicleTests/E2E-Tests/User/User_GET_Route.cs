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
        private class TokenHolder{
            public string Token { get; set; }
        }
        public async Task<string> AppendJWTHeader(DatabaseContext db, HttpClient client, User user) {
            db.Users.Add(user);
            db.SaveChanges();

            //handle login for JWT Token
            StringContent query = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var loginResponse = await client.PostAsync("/api/auth/login", query);
            var content = await loginResponse.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TokenHolder>(content,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.Token);

            return result.Token;
        }
         
        [Fact]
        public async Task Should_ReturnAnEmpyList_WhenUserDBIsEmpty()
        {
            //Given the User db is empty
            var testServer = new TestServer(hostBuilder(System.Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();
            
            //When a list of users is requested by the api
            var response = await client.GetAsync("/User");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<object>>(content);

            //Then return an empty list
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task Should_ReturnCode200_WhenRquestHasValidJWTToken()
        {
        //Given that the service is running
        var testServer = new TestServer(hostBuilder(System.Guid.NewGuid().ToString()));
        var client = testServer.CreateClient();
        var db = testServer.Services.GetRequiredService<DatabaseContext>();
        db.Database.EnsureDeleted();

        var user = new User{
            UserName = "johndoe",
            Email = "test@test.com",
            Id = 2,
        };
        await AppendJWTHeader(db, client, user);
        //When GET is called with a paramater of an ID
        var response = await client.GetAsync("/User/Profile");
        //Then return a 200 code
        response.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_ReturnUserInfo_WhenRequestHasValidJWTToken()
        {
        //Given that the service is running
        var testServer = new TestServer(hostBuilder(System.Guid.NewGuid().ToString()));
        var client = testServer.CreateClient();
        var db = testServer.Services.GetRequiredService<DatabaseContext>();
        db.Database.EnsureDeleted();

        var user = new User{
            UserName = "johndoe",
            Name = "John Doe",
            Email = "test@test.com",
            Id = 2,
        };
        await AppendJWTHeader(db, client, user);
        //When GET is called with a paramater of an ID
        var response = await client.GetAsync("/User/Profile");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<User>(content,
        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        //Then return a 200 code
        result.Should().BeEquivalentTo(user);
        }
    }
}