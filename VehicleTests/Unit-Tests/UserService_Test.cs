using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using VehicleDatabase.Data;
using VehicleWebAPI.Models;
using VehicleWebAPI.Services;
using Xunit;
using FluentAssertions;

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
            MockContext.Setup(p => p.SaveChanges()).Returns(1);
            UserDatabaseService UserService = new UserDatabaseService(MockContext.Object);


            //When GetAllVehicles is called
            UserService.AddUser(user);
        
            //Then the database context method to grab the vehicles
            //should be called
            MockContext.Verify(db => db.Users.Add(user), Times.Once());
            MockContext.Verify(db => db.SaveChanges(), Times.AtLeastOnce());
        }

        [Fact]
        public void CheckIfEmailAlreadyExistsSuccessfully()
        {
        //Given
            var userSet = new List<User> {
                new User
                {
                    Id = 1,
                    Vehicles = new List<Vehicle>{},
                    Email = "testuser@test.org",
                    PhoneNumber = "192-875-3246",
                    Authorization = User.AuthLevel.User,
                    UserName = "testperson",
                    Password = "testpass",
                }
            };

            var queryableUserSet = userSet.AsQueryable();

            Mock<DbSet<User>> MockSet = new Mock<DbSet<User>>();
            MockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryableUserSet.Provider);
            MockSet.As<IQueryable<User>>().Setup(m => m.Expression).Callback(() => {}).Returns(queryableUserSet.Expression);
            MockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryableUserSet.ElementType);
            MockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(queryableUserSet.GetEnumerator);
            Mock<DatabaseContext> MockContext = new Mock<DatabaseContext>();
            MockContext.Setup(p => p.Users).Returns(MockSet.Object);
            UserDatabaseService UserService = new UserDatabaseService(MockContext.Object);

        //When
            var success = UserService.EmailExists("testuser@test.org");
            var failure = UserService.EmailExists("123213@asdasd.org");
        
        //Then
            MockContext.Verify(p => p.Users, Times.Exactly(2));
            success.Should().BeTrue();
            failure.Should().BeFalse();
        }

        [Fact]
        public void CheckIfUsernameExistsShouldBeCalledInTheService()
        {
        //Given
            var userSet = new List<User> {
                new User
                {
                    Id = 1,
                    Vehicles = new List<Vehicle>{},
                    Email = "testuser@test.org",
                    PhoneNumber = "192-875-3246",
                    Authorization = User.AuthLevel.User,
                    UserName = "testperson",
                    Password = "testpass",
                }
            };

            var queryableUserSet = userSet.AsQueryable();

            Mock<DbSet<User>> MockSet = new Mock<DbSet<User>>();
            MockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryableUserSet.Provider);
            MockSet.As<IQueryable<User>>().Setup(m => m.Expression).Callback(() => {}).Returns(queryableUserSet.Expression);
            MockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryableUserSet.ElementType);
            MockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(queryableUserSet.GetEnumerator);
            Mock<DatabaseContext> MockContext = new Mock<DatabaseContext>();
            MockContext.Setup(p => p.Users).Returns(MockSet.Object);

            UserDatabaseService UserService = new UserDatabaseService(MockContext.Object);
        //When

            var success = UserService.UsernameExists("testperson");
            var failure = UserService.UsernameExists("123213@asdasd.org");
        
        //Then
            MockContext.Verify(p => p.Users, Times.Exactly(2));

            success.Should().BeTrue();
            failure.Should().BeFalse();
        }

        [Fact]
        public void VerifyCredentialsShouldBeCalledInTheService()
        {
        //Given
            var userSet = new List<User> {
                new User
                {
                    Id = 1,
                    Vehicles = new List<Vehicle>{},
                    Email = "testuser@test.org",
                    PhoneNumber = "192-875-3246",
                    Authorization = User.AuthLevel.User,
                    UserName = "testperson",
                    Password = "testpass",
                }
            };

            var queryableUserSet = userSet.AsQueryable();

            Mock<DbSet<User>> MockSet = new Mock<DbSet<User>>();
            MockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryableUserSet.Provider);
            MockSet.As<IQueryable<User>>().Setup(m => m.Expression).Callback(() => {}).Returns(queryableUserSet.Expression);
            MockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryableUserSet.ElementType);
            MockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(queryableUserSet.GetEnumerator);
            Mock<DatabaseContext> MockContext = new Mock<DatabaseContext>();
            MockContext.Setup(p => p.Users).Returns(MockSet.Object);

            UserDatabaseService UserService = new UserDatabaseService(MockContext.Object);
        //When

            var success = UserService.VerifyCredentials(new User {UserName = "testperson", Password = "testpass"});
            var failure = UserService.VerifyCredentials(new User {UserName = "among", Password = "us"});
        
        //Then
            MockContext.Verify(p => p.Users, Times.Exactly(2));

            success.Should().BeTrue();
            failure.Should().BeFalse();
        }
    }
}