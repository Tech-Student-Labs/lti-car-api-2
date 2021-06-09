using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using VehicleDatabase.Data;
using VehicleWebAPI.Models;
using VehicleWebAPI.Services;
using Xunit;

namespace VehicleTests.Unit_Tests
{
    public class VehicleService_Test
    {
        [Fact]
        public void GetAllVehiclesMethod_ShouldCallDBContextMethod_ToGetFirst20Vehicles()
        {
        //Given the service has been injected with the database context
        var vehicles = new List<Vehicle> {
            new Vehicle
                {
                    Id = 1,
                    VIN = "4Y1SL65848Z411439", Make = "Toyota", Model = "Corolla", Year = "1997", Miles = 145000,
                    Color = "Silver", SellingPrice = 2000, Status = Vehicle.StatusCode.Inventory, UserId = 1
                },
            new Vehicle
                {
                    Id = 2,
                    VIN = "5Z1SL39746U411411", Make = "Honda", Model = "Civic", Year = "1997", Miles = 145000,
                    Color = "Black", SellingPrice = 3000, Status = Vehicle.StatusCode.Inventory, UserId = 1
                },
            new Vehicle
                {
                    Id = 3,
                    VIN = "7T1SL646726411440", Make = "Subaru", Model = "Impreza", Year = "2005", Miles = 175000,
                    Color = "Blue", SellingPrice = 4000, Status = Vehicle.StatusCode.Inventory, UserId = 1
                }
        };
        var queryableVehicles = vehicles.AsQueryable();

        Mock<DbSet<Vehicle>> MockSet = new Mock<DbSet<Vehicle>>();
        MockSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(queryableVehicles.Provider);
        MockSet.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Callback(() => {}).Returns(queryableVehicles.Expression);
        MockSet.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(queryableVehicles.ElementType);
        MockSet.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(queryableVehicles.GetEnumerator);
        Mock<DatabaseContext> MockContext = new Mock<DatabaseContext>();
        MockContext.Setup(p => p.Vehicles).Returns(MockSet.Object);
        VehicleDatabaseService VehicleService = new VehicleDatabaseService(MockContext.Object);


        //When GetAllVehicles is called
        VehicleService.GetAllVehicles();
        
        //Then the database context method to grab the vehicles
        //should be called
        MockContext.Verify(db => db.Vehicles, Times.Once());
        }
    }
}