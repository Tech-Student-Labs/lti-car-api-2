using FluentAssertions;
using VehicleWebAPI.Models;
using Xunit;

namespace VehicleTests.Models
{
    public class User_Test
    {
        [Fact]
        public void Should_CreateAnInstanceOfUser()
        {
            //Given
            
            //When
            User user = new User();
            //Then
            user.Should().NotBeNull();

        }
    }
}