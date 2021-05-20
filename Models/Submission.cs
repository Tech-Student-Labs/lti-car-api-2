namespace Models
{
    public class Submission
    {
        string Status { get; set; }

        IVehicle vehicle { get; set; }

        int userID { get; set; }
    }
}