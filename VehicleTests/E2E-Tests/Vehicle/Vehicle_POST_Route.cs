using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
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
    public class Vehicle_POST_Route
    {
        private IWebHostBuilder hostBuilder => new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location))
            .UseStartup<Startup>()
            .ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<DatabaseContext>)));
                services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("VehiclePost"));
            });

        private class TokenHolder
        {
            public string Token { get; set; }
        }

        public async Task<string> AppendJWTHeader(DatabaseContext db, HttpClient client, string username = "johndoe")
        {
            db.Users.Add(new User {Email = "johndoe@test.com", UserName = username, Password = "def@123"});
            db.SaveChanges();

            //handle login for JWT Token
            var user = new User {Email = "johndoe2@test.com", UserName = username, Password = "def@123"};
            StringContent query = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var loginResponse = await client.PostAsync("/api/auth/login", query);
            var content = await loginResponse.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TokenHolder>(content,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.Token);

            return result.Token;
        }

        [Fact]
        public async Task Should_Return201_WhenPostRequestIsSubmitted()
        {
            // GIVEN the service is running and there are no vehicles in vehicle list
            var testServer = new TestServer(hostBuilder);
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            db.Database.EnsureDeleted();

            var client = testServer.CreateClient();
            var clean = JsonSerializer.Serialize(new Vehicle
            {
                Id = 1,
                VIN = "4Y1SL65848Z411439",
                Make = "Toyota",
                Model = "Corolla",
                Year = 1997,
                Miles = 145000,
                Color = "Silver",
                SellingPrice = 2000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            StringContent query = new StringContent(clean, Encoding.UTF8, "application/json");
            await AppendJWTHeader(db, client);

            // WHEN a POST request is submitted to the vehicle db 
            var response = await client.PostAsync("/vehicle", query);
            db.SaveChanges();

            // THEN the response body should return a 201 code
            response.StatusCode.Should().Be(201);
            db.Database.EnsureDeleted();
        }


        [Fact]
        public async Task Should_CreateVehicleInDB_WhenPostRequestIsSubmitted()
        {
            // GIVEN the service is running and there are no vehicles in vehicle list
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            await db.Database.EnsureDeletedAsync();

            FileStream fs = File.Open("../../../Resources/sedan-car-front-2.png", FileMode.Open);
            MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);
            string imageBase64Data =
                Convert.ToBase64String(ms.ToArray());
            string imageDataURL =
                string.Format("data:image/jpg;base64,{0}",
                    imageBase64Data);
            var vehicleImage = new VehicleImage {Id = 1, VehicleId = 1, ImageData = imageDataURL};
            ms.Close();
            fs.Close();
            await ms.DisposeAsync();
            await fs.DisposeAsync();

            var vehicle1 = new Vehicle
            {
                Id = 1,
                VIN = "4Y1SL65848Z411439",
                Make = "Toyota",
                Model = "Corolla",
                Year = 1997,
                Miles = 145000,
                Color = "Silver",
                SellingPrice = 2000,
                UserId = 1,
                Status = Vehicle.StatusCode.Inventory,
                VehicleImages = new List<VehicleImage> {vehicleImage}
            };
            await AppendJWTHeader(db, client);
            // WHEN a POST request is submitted to the vehicle db
            await client.PostAsJsonAsync("/Vehicle", vehicle1);

            // THEN the response body should return the amount of vehicles in db
            var dbVehicle = db.Vehicles.Include(v => v.VehicleImages).FirstOrDefault(t => t.Id == vehicle1.Id);
            dbVehicle.User = null;
            dbVehicle.Should().BeEquivalentTo(vehicle1);
            await db.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_Return201_WhenCreated_AndThereAlreadyExists1Vehicle()
        {
            //   GIVEN the service is running and there is 1 vehicle in vehicle list
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            await db.Database.EnsureDeletedAsync();

            var vehicleToAdd = new Vehicle
            {
                VIN = "YU1SL658486123463",
                Make = "Mitsubishi",
                Model = "Eclipse",
                Year = 2005,
                Miles = 75000,
                Color = "Purple",
                SellingPrice = 6000,
                Status = Vehicle.StatusCode.Sold,
                UserId = 1
            };

            // populate our db with one vehicle
            db.Add(new Vehicle
            {
                Id = 1,
                VIN = "4Y1SL65848Z411439",
                Make = "Toyota",
                Model = "Corolla",
                Year = 1997,
                Miles = 145000,
                Color = "Silver",
                SellingPrice = 2000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            db.SaveChanges();
            await AppendJWTHeader(db, client);

            // WHEN a POST request is submitted to the vehicle db
            var postResponse = await client.PostAsJsonAsync("/vehicle", vehicleToAdd);

            // THEN the response body should return a 201 code
            postResponse.StatusCode.Should().Be(201);
            await db.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_CreateVehicleInDB_WhenPostIsSubmittedAnd1VehicleExists()
        {
            //   GIVEN the service is running and there is 1 vehicle in vehicle list
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            await db.Database.EnsureDeletedAsync();

            var vehicleToAdd = new Vehicle
            {
                VIN = "YU1SL658486123463",
                Make = "Mitsubishi",
                Model = "Eclipse",
                Year = 2005,
                Miles = 75000,
                Color = "Purple",
                SellingPrice = 6000,
                Status = Vehicle.StatusCode.Sold,
                UserId = 1
            };

            db.Add(new Vehicle
            {
                Id = 1,
                VIN = "4Y1SL65848Z411439",
                Make = "Toyota",
                Model = "Corolla",
                Year = 1997,
                Miles = 145000,
                Color = "Silver",
                SellingPrice = 2000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            db.SaveChanges();
            await AppendJWTHeader(db, client);

            // WHEN a POST request is submitted to the vehicle db 
            var postResponse = await client.PostAsJsonAsync("/vehicle", vehicleToAdd);

            //   THEN the response body should return the count of vehicles in db
            db.Vehicles.ToList().Count.Should().Be(2);
            await db.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_Return200_WhenCreateVehicle_AndManyVehiclesExist()
        {
            //   GIVEN the service is running and there are many vehicles in vehicle list
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            await db.Database.EnsureDeletedAsync();

            var vehicleToAdd = new Vehicle
            {
                VIN = "YU1SL658486123463",
                Make = "Mitsubishi",
                Model = "Eclipse",
                Year = 2005,
                Miles = 75000,
                Color = "Purple",
                SellingPrice = 6000,
                Status = Vehicle.StatusCode.Sold,
                UserId = 1
            };

            // populate db with vehicles
            db.Add(new Vehicle
            {
                VIN = "4Y1SL65848Z411439",
                Make = "Toyota",
                Model = "Corolla",
                Year = 1997,
                Miles = 145000,
                Color = "Silver",
                SellingPrice = 2000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            db.Add(new Vehicle
            {
                VIN = "5Z1SL39746U411411",
                Make = "Honda",
                Model = "Civic",
                Year = 1997,
                Miles = 145000,
                Color = "Black",
                SellingPrice = 3000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            db.Add(new Vehicle
            {
                VIN = "7T1SL646726411440",
                Make = "Subaru",
                Model = "Impreza",
                Year = 2005,
                Miles = 175000,
                Color = "Blue",
                SellingPrice = 4000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            db.Add(new Vehicle
            {
                VIN = "9P1SL658486268352",
                Make = "Mazda",
                Model = "3",
                Year = 2007,
                Miles = 200000,
                Color = "Red",
                SellingPrice = 2000,
                Status = Vehicle.StatusCode.Pending,
                UserId = 1
            });
            db.SaveChanges();
            await AppendJWTHeader(db, client);

            // WHEN a POST request is submitted to the vehicle db 
            var postResponse = await client.PostAsJsonAsync("/vehicle", vehicleToAdd);

            //   THEN the response body should return the count of vehicles in db
            postResponse.StatusCode.Should().Be(201);
            await db.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_AddVehicleToDatabase_WhenPostRequestIsMade()
        {
            //   GIVEN the service is running and there are many vehicles in vehicle list
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            await db.Database.EnsureDeletedAsync();

            var vehicleToAdd = new Vehicle
            {
                VIN = "YU1SL658486123463",
                Make = "Mitsubishi",
                Model = "Eclipse",
                Year = 2005,
                Miles = 75000,
                Color = "Purple",
                SellingPrice = 6000,
                Status = Vehicle.StatusCode.Sold,
                UserId = 1
            };

            // populate db with vehicles
            db.Add(new Vehicle
            {
                VIN = "4Y1SL65848Z411439",
                Make = "Toyota",
                Model = "Corolla",
                Year = 1997,
                Miles = 145000,
                Color = "Silver",
                SellingPrice = 2000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            db.Add(new Vehicle
            {
                VIN = "5Z1SL39746U411411",
                Make = "Honda",
                Model = "Civic",
                Year = 1997,
                Miles = 145000,
                Color = "Black",
                SellingPrice = 3000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            db.Add(new Vehicle
            {
                VIN = "7T1SL646726411440",
                Make = "Subaru",
                Model = "Impreza",
                Year = 2005,
                Miles = 175000,
                Color = "Blue",
                SellingPrice = 4000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            db.Add(new Vehicle
            {
                VIN = "9P1SL658486268352",
                Make = "Mazda",
                Model = "3",
                Year = 2007,
                Miles = 200000,
                Color = "Red",
                SellingPrice = 2000,
                Status = Vehicle.StatusCode.Pending,
                UserId = 1
            });
            db.SaveChanges();
            await AppendJWTHeader(db, client);

            // WHEN a POST request is submitted to the vehicle db 
            await client.PostAsJsonAsync("/vehicle", vehicleToAdd);

            //   THEN the response body should return the count of vehicles in db
            db.Vehicles.ToList().Count.Should().Be(5);
            await db.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_ReturnInvalid_WhenAddingInvalidVehicle()
        {
            //Given the service is running and there are no vehicles in vehicle list
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            await db.Database.EnsureDeletedAsync();

            var vehicleToAdd = new Vehicle
            {
                Make = "Mitsubishi",
                Model = "Eclipse"
            };
            await AppendJWTHeader(db, client);

            //When
            var postResponse = await client.PostAsJsonAsync("/vehicle", vehicleToAdd);

            //Then the response body should return invalid
            postResponse.StatusCode.Should().Be(400);
            await db.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_ReturnInvalid_WhenAddingAVehicleWithExistingId()
        {
            //Given the service is running and there are no vehicles in vehicle list
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            await db.Database.EnsureDeletedAsync();

            var vehicleToAdd = new Vehicle
            {
                Id = 1,
                VIN = "YU1SL658486123463",
                Make = "Mitsubishi",
                Model = "Eclipse",
                Year = 2005,
                Miles = 75000,
                Color = "Purple",
                SellingPrice = 6000,
                Status = Vehicle.StatusCode.Sold,
                UserId = 1
            };

            db.Add(new Vehicle
            {
                Id = 1,
                VIN = "YU1SL658486123463",
                Make = "Mitsubishi",
                Model = "Eclipse",
                Year = 2005,
                Miles = 75000,
                Color = "Purple",
                SellingPrice = 6000,
                Status = Vehicle.StatusCode.Sold,
                UserId = 1
            });
            db.SaveChanges();
            await AppendJWTHeader(db, client);

            //When
            var postResponse = await client.PostAsJsonAsync("/vehicle", vehicleToAdd);

            //Then the response body should return invalid
            postResponse.StatusCode.Should().Be(400);
            await db.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Should_ReturnInvalid_WhenAddingAVehicleWithExistingVIN()
        {
            //Given the service is running and there are no vehicles in vehicle list
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();
            var db = testServer.Services.GetRequiredService<DatabaseContext>();
            await db.Database.EnsureDeletedAsync();

            var vehicleToAdd = new Vehicle
            {
                VIN = "5Z1SL39746U411411",
                Make = "Honda",
                Model = "Civic",
                Year = 1997,
                Miles = 145000,
                Color = "Black",
                SellingPrice = 3000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            };

            db.Add(new Vehicle
            {
                VIN = "5Z1SL39746U411411",
                Make = "Honda",
                Model = "Civic",
                Year = 1997,
                Miles = 145000,
                Color = "Black",
                SellingPrice = 3000,
                Status = Vehicle.StatusCode.Inventory,
                UserId = 1
            });
            db.SaveChanges();
            await AppendJWTHeader(db, client);

            //When
            var postResponse = await client.PostAsJsonAsync("/vehicle", vehicleToAdd);

            //Then the response body should return invalid
            postResponse.StatusCode.Should().Be(400);
            await db.Database.EnsureDeletedAsync();
        }
    }
}