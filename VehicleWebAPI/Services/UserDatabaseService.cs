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
            if (!this.UsernameExists(user.UserName)) {
                _db.Users.Add(user);
                _db.SaveChanges();
                return user;
            }
            else {
                return null;
            }

        }

        public bool UsernameExists(string username) 
        {
            return _db.Users.Any(u => u.UserName == username);
        }

        public bool VerifyCredentials(User user)
        {
            return _db.Users.Any(u => u.UserName == user.UserName && u.Password == user.Password);
        }
    }
}