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
    public class InventoryService_Test
    {
        [Fact]
        public void GetInventoryVehiclesMethod_ShouldCallDBContextMethod_ToGetFirstAllVehicles()
        {
        //Given the service has been injected with the database context
        var inventory = new List<InventoryVehicle> {
            new InventoryVehicle
                {
                    Id = 1,
                    VehicleId = 1,
                    Vehicle = new Vehicle 
                    {
                        Id = 1,
                        VIN = "4Y1SL65848Z411439", Make = "Toyota", Model = "Corolla", Year = 1997, Miles = 145000,
                        Color = "Silver", SellingPrice = 2000, Status = Vehicle.StatusCode.Inventory, UserId = 1 
                    },
                    Price = 3000
                },
            new InventoryVehicle
                {
                    Id = 2,
                    VehicleId = 2,
                    Vehicle = new Vehicle
                    {
                        Id = 2,
                        VIN = "5Z1SL39746U411411", Make = "Honda", Model = "Civic", Year = 1997, Miles = 145000,
                        Color = "Black", SellingPrice = 3000, Status = Vehicle.StatusCode.Inventory, UserId = 1
                    },
                    Price = 3800
                },
            new InventoryVehicle
                {
                    Id = 3,
                    VehicleId = 3,
                    Vehicle = new Vehicle
                    {
                        Id = 3,
                        VIN = "7T1SL646726411440", Make = "Subaru", Model = "Impreza", Year = 2005, Miles = 175000,
                        Color = "Blue", SellingPrice = 4000, Status = Vehicle.StatusCode.Inventory, UserId = 1
                    },
                    Price = 5200
                }
        };
        var queryableInventory = inventory.AsQueryable();

        Mock<DbSet<InventoryVehicle>> MockSet = new Mock<DbSet<InventoryVehicle>>();
        MockSet.As<IQueryable<InventoryVehicle>>().Setup(m => m.Provider).Returns(queryableInventory.Provider);
        MockSet.As<IQueryable<InventoryVehicle>>().Setup(m => m.Expression).Callback(() => {}).Returns(queryableInventory.Expression);
        MockSet.As<IQueryable<InventoryVehicle>>().Setup(m => m.ElementType).Returns(queryableInventory.ElementType);
        MockSet.As<IQueryable<InventoryVehicle>>().Setup(m => m.GetEnumerator()).Returns(queryableInventory.GetEnumerator);
        Mock<DatabaseContext> MockContext = new Mock<DatabaseContext>();
        MockContext.Setup(p => p.Inventory).Returns(MockSet.Object);
        InventoryDatabaseService InventoryService = new InventoryDatabaseService(MockContext.Object);


        //When GetAllVehicles is called
        InventoryService.GetInventoryVehicles();
        
        //Then the database context method to grab the vehicles
        //should be called
        MockContext.Verify(db => db.Inventory, Times.Once());
        }
    }
}