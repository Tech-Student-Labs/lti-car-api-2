namespace Models
{
    public interface IVehicle
    {
        string Make { get; set; }
        string Model { get; set; }
        string Year { get; set; }
        int Miles { get; set; }
        string Color { get; set; }
        string[] Images { get; set; }
        string VIN { get; set; }
        double sellingPrice { get; set; }
    }
}