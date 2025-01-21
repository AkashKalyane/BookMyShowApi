using System.Security.Cryptography;
using System.Text;
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
        public async Task<User> GetUserByName(string Name)
        {
            var user = await _context.Users.Where(x => x.UserName == Name).FirstOrDefaultAsync();
            return user;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task AddUser(User user)
        {
            //var userDetail = new UserDetail()
            //{
            //    UserDetailId = user.UserId,
            //    FirstName = "test45",
            //    MiddleName = "test45",
            //    LastName = "test45",
            //    CreatedBy = 1,
            //    CreateOn = DateTime.Now
            //};
            //user.UserDetailUsers.Add(userDetail);
            var temp = new User()
            {
                UserContactUsers = new List<UserContact>()
                {
                    new UserContact()
                    {
                        PhoneNumber = "8104702072",
                        CountryCode = "IN"
                    }
                },

            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public (string passwordHash, string salt) CreatePasswordHash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            var hash = Convert.ToBase64String(Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    salt,
                    iterations: 10000,
                    HashAlgorithmName.SHA512,
                    64
                    )
                );
            return (hash, Convert.ToBase64String(salt));
        }
        
        public bool VerifyPassword(string password, string passwordHash, string salt)
        {
            byte[] bytes = Convert.FromBase64String(salt);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                   Encoding.UTF8.GetBytes(password),
                    bytes,
                    iterations: 10000,
                    HashAlgorithmName.SHA512,
                    64
                );

            string toCheck = Convert.ToBase64String(hashToCompare);
            return (passwordHash == toCheck);
        }
    }
}
