namespace Models
{
    public interface IInventoryVehicle
    {
        Vehicle Vehicle { get; set; }
        int VehicleVIN { get; set; }
        double Price { get; set; }
    }
}