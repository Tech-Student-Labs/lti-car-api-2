namespace Models
{
    public class InventoryVehicle : IInventoryVehicle
    {
        public Vehicle Vehicle { get; set; }
        public int VehicleVIN { get; set; }
        public double Price { get; set; }
    }
}