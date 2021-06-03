using FluentAssertions;
using VehicleWebAPI.Models;
using Xunit;

namespace VehicleTests.Models
{
    public class VehicleTest
    {
        [Fact]
        public void Should_CreateAnInstanceOfVehicle()
        {
            Vehicle vehicle = new Vehicle();
            vehicle.Should().NotBeNull();
        }
    }
}