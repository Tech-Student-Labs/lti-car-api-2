using System.Collections.Generic;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
    public interface IUserDatabaseService
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public User AddUser(User user);
        public bool UsernameExists(string username);
        public User VerifyCredentials(User user);
        public bool EmailExists(string email);
    }
}