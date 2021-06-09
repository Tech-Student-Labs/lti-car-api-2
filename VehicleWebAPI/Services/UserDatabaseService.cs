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
    }
}