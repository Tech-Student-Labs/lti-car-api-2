namespace Models
{
    public class VehicleImage : IVehicleImage
    {
        public int VehicleVIN { get; set; }
        public Vehicle Vehicle { get; set; }
        public string PhotoURL { get; set; }
    }
}