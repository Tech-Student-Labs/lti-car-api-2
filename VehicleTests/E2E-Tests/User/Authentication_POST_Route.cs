using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Net.Http;
using System.Text;
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


    public class AuthenticationPostRoute
    {

        private class TokenHolder{
            public string Token { get; set; }
        }
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
        public async Task Should_Return200_WhenMatchLoginToUser()
        {
            //Given User exists in User table
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();

            //WHEN POST request is made with correct username/password for User
            var login = JsonSerializer.Serialize(new User{UserName = "johndoe", Password = "def@123"});
            StringContent query = new StringContent(login, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/auth/login", query);

            //THEN Response should return code 200
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_ReturnJWTToken_WhenMatchLoginToUser()
        {
            //Given User exists in User Table
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();
            
            //When POST request is made with correct username/password for User
            var login = JsonSerializer.Serialize(new User{UserName = "johndoe", Password = "def@123"});
            StringContent query = new StringContent(login, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/login", query);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TokenHolder>(content,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            
            //Then Response should return the JWT token
            result.Token.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Should_Return401_WhenFailToMatchLoginToUser()
        {
            //Given User exists in User Table
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();
        
            //When POST request is made with invalid username/password for User
            var login = JsonSerializer.Serialize(new User{UserName = "invalid", Password = "entry"});
            StringContent query = new StringContent(login, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/auth/login", query);
        
            //Then Response should return code 401
            result.StatusCode.Should().Be(401);
        }

        [Fact]
        public async Task Should_ReturnNoContent_WhenFailToMatchLoginToUser()
        {
            //Given User exists in User Table
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();
            
            //When POST request is made with invalid username/password for User
            var login = JsonSerializer.Serialize(new User{UserName = "invalid", Password = "entry"});
            StringContent query = new StringContent(login, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/login", query);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TokenHolder>(content,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            
            //Then Response should return null for the Token
            result.Token.Should().BeNull();
        }

    }
}
