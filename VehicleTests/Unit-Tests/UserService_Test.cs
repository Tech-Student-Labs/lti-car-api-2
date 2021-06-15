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
    public class UserService_Test
    {
        [Fact]
        public void AddingAUser_ShouldCallAddUserFunctionInService()
        {
            //Given the service has been injected with the database context
            var userSet = new List<User> {};

            var user = new User
                {
                    Id = 1,
                    Vehicles = new List<Vehicle>{},
                    Email = "testuser@test.org",
                    PhoneNumber = "192-875-3246",
                    Authorization = User.AuthLevel.User,
                    UserName = "testperson",
                    Password = "testpass",
                };
            var queryableUserSet = userSet.AsQueryable();

            Mock<DbSet<User>> MockSet = new Mock<DbSet<User>>();
            MockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryableUserSet.Provider);
            MockSet.As<IQueryable<User>>().Setup(m => m.Expression).Callback(() => {}).Returns(queryableUserSet.Expression);
            MockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryableUserSet.ElementType);
            MockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(queryableUserSet.GetEnumerator);
            Mock<DatabaseContext> MockContext = new Mock<DatabaseContext>();
            MockContext.Setup(p => p.Users).Returns(MockSet.Object);
            MockContext.Setup(p => p.Add(It.IsAny<User>())).Callback((User user) => userSet.Add(user));
            UserDatabaseService UserService = new UserDatabaseService(MockContext.Object);


            //When GetAllVehicles is called
            UserService.AddUser(user);
        
            //Then the database context method to grab the vehicles
            //should be called
            MockContext.Verify(db => db.Users.Add(user), Times.Once());
        }
    }
}