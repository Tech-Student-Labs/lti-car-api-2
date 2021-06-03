namespace Models
{
    public interface IInventoryVehicle
    {
        int Id { get; set; }
        int VehicleId { get; set; }
        double Price { get; set; }
    }
}