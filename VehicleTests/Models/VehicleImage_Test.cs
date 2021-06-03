using FluentAssertions;
using Models;
using VehicleDatabase.Models;
using Xunit;

namespace VehicleTests
{
    public class VehicleImage_Test
    {
        [Fact]
        public void Action_Should_When()
        {
            //Given

            //When
            VehicleImage vehicleImage = new VehicleImage();
            //Then
            vehicleImage.Should().NotBeNull();

        }
    }
}