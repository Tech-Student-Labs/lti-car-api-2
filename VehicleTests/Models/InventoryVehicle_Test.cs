using FluentAssertions;
using VehicleDatabase.Models;
using Xunit;

namespace VehicleTests
{
    public class InventoryVehicle_Test
    {
        [Fact]
        public void Action_Should_When()
        {
            //Given
            InventoryVehicle inventoryVehicle = new InventoryVehicle();
            //When

            //Then
            inventoryVehicle.Should().NotBeNull();

        }
    }
}