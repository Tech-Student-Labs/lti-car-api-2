using System.Collections.Generic;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
    public interface IUserDatabaseService
    {
        public List<User> GetAllUsers();
        public User AddUser(User user);
        public bool UsernameExists(string username);
        public bool VerifyCredentials(User user);
    }
}