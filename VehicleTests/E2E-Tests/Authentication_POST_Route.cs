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
        public async Task test()
        {
            //Given
            var testServer = new TestServer(hostBuilder(Guid.NewGuid().ToString()));
            var client = testServer.CreateClient();

            //WHEN
            var login = JsonSerializer.Serialize(new LoginModel{UserName = "johndoe", Password = "def@123"});
            StringContent query = new StringContent(login, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/api/auth/login", query);

            //THEN
            result.StatusCode.Should().Be(200);
        }

    }
}