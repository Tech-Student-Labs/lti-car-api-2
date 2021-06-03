using System;
using FluentAssertions;
using Models;
using VehicleDatabase.Models;
using Xunit;

namespace VehicleTests
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