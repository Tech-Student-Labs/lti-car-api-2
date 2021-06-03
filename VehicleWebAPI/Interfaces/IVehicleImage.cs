namespace VehicleWebAPI.Interfaces
{
    public interface IVehicleImage
    {
        int Id { get; set; }
        int VehicleId { get; set; }
        byte[] ImageData { get; set; }
    }
}