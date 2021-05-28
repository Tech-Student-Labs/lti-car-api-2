namespace Models
{
    public interface IVehicleImage
    {
        int VehicleVIN { get; set; }
        Vehicle Vehicle { get; set; }
        string PhotoURL { get; set; }
    }
}