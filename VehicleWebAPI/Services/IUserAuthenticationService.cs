using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
    public interface IUserAuthenticationService
    {
        string Authenticate(User user);
    }
}