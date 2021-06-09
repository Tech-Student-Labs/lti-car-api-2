using System.Collections.Generic;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
    public interface IUserDatabaseService
    {
        public List<User> GetAllUsers();
    }
}