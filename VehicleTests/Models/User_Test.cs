using FluentAssertions;
using Models;
using Xunit;

namespace VehicleTests
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