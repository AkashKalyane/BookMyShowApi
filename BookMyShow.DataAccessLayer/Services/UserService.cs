using System.Security.Cryptography;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.DataContext;
using BookMyShow.DataAccessLayer.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.DataAccessLayer.Services
{
    public class UserService : IUserService
    {
        private BookMyShowContext _context;
        public UserService(BookMyShowContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task AddUser(User user)
        {
            //    var passwordHash = CreatePasswordHashWithSlat(user.Password);
            //    user.Password = passwordHash.PasswordHash;
            //    user.Salt = passwordHash.Slat;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public (string PasswordHash, string Slat) CreatePasswordHashWithSlat(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                )
            );
            return (hash, Convert.ToBase64String(salt));
        }
    }
}
