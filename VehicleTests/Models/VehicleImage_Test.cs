using FluentAssertions;
using VehicleWebAPI.Models;
using Xunit;

namespace VehicleTests
{
    public class VehicleImage_Test
    {
        [Fact]
        public void VehicleImage_ShouldBeInstantiated()
        {
            //Given

            //When
            VehicleImage vehicleImage = new VehicleImage();
            //Then
            vehicleImage.Should().NotBeNull();

        }
    }
}