using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
        Task UpdateUser();
        Task DeleteUser(User user);
        (string passwordHash, string salt) CreatePasswordHash(string password);
        bool VerifyPassword(string password, string passwordHash, string salt);
    }
}
