using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VehicleDatabase.Data;
using VehicleWebAPI.Interfaces;
using VehicleWebAPI.Models;

namespace VehicleWebAPI.Services
{
    public class UserDatabaseService : IUserDatabaseService
    {
        private readonly DatabaseContext _db;

        public UserDatabaseService(DatabaseContext db)
        {
            _db = db;
        }
        
        public List<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public User AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public bool UsernameExists(string username) 
        {
            return _db.Users.Any(u => u.UserName == username);
        }

        public bool EmailExists(string email) 
        {
            return _db.Users.Any(u => u.Email == email);
        }

        public User VerifyCredentials(User user)
        {
            return _db.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
        }
    }
}