using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class UserManager
    {
        private IUserService _userService;
        public UserManager(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var result = await _userService.GetUsers();
            return result.Select(x => UserDto.MapToDto(x)).ToList();
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            if (result == null) throw new Exception("User doesn't exist for the specified id");
            return UserDto.MapToDto(result);
        }

        public async Task AddUser(UserDto userDto)
        {
            var exceptions = new List<string>();

            var inputUserName = userDto.UserName.Trim();
            var inputEmail = userDto.Email.Trim();
            var inputPassword = userDto.Password.Trim();

            if (inputUserName.Length < 3) exceptions.Add("User name must be greater than or equal to 3 characters");

            if (inputPassword.Length < 8) exceptions.Add("Password must be greater than or equal to 8 characters");

            bool isEmail = Regex.IsMatch(inputEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (!isEmail) exceptions.Add("Not a valid email");

            var IsEmailExist = await _userService.GetUserByEmail(inputEmail);

            if (IsEmailExist != null) exceptions.Add("Email already exists");

            var UserNameExist = await _userService.GetUserByName(inputUserName);

            if (UserNameExist != null) exceptions.Add("User name already exists");

            if (exceptions.Count != 0) { throw new CustomException(exceptions); }

            var passwordHashAndSalt = _userService.CreatePasswordHash(inputPassword);
            var user = new User
            {
                UserName = inputUserName,
                Email = inputEmail,
                Password = passwordHashAndSalt.passwordHash,
                Salt = passwordHashAndSalt.salt,
                CreatedBy = "akash"
            };
            await _userService.AddUser(user);
        }

        public async Task UpdateUser(int id, UserDto userDto)
        {
            var exceptions = new List<string>();

            var inputUserName = userDto.UserName.Trim();
            var inputEmail = userDto.Email.Trim();
            var inputPassword = userDto.Password.Trim();

            if (inputUserName == null) { exceptions.Add("User name is required"); }
            if (inputEmail == null) { exceptions.Add("Email is required"); }
            if (inputPassword == null) { exceptions.Add("Password is required"); }

            if (inputUserName.Length < 3) exceptions.Add("User name must be greater than or equal to 3 characters");
            if (inputPassword.Length < 8) exceptions.Add("Password must be greater than or equal to 8 characters");

            bool isEmail = Regex.IsMatch(inputEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail) exceptions.Add("Not a valid email");



            var user = await _userService.GetUserById(id);
            if (user == null) exceptions.Add("User does not exist for the provided id");

            var UserNameExist = await _userService.GetUserByName(inputUserName);
            if (UserNameExist != null && !(user.UserName == inputUserName)) exceptions.Add("User name already exists");

            var IsEmailExist = await _userService.GetUserByEmail(inputEmail);
            if (IsEmailExist != null && !(user.Email == inputEmail)) exceptions.Add("Email already exists");

            if (exceptions.Count != 0) { throw new CustomException(exceptions); };

            var passwordHashAndSalt = _userService.CreatePasswordHash(inputPassword);
            user.UserName = inputUserName;
            user.Email = inputEmail;
            user.Password = passwordHashAndSalt.passwordHash;
            user.Salt = passwordHashAndSalt.salt;

            await _userService.UpdateUser();
        }

        public async Task LoginUser(EmailAndPasswordDto EP)
        {
            var exceptions = new List<string>();

            var inputEmail = EP.Email.Trim();
            var inputPassword = EP.Password.Trim();

            bool isEmail = Regex.IsMatch(inputEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (inputPassword.Length < 8) exceptions.Add("Password must be greater than or equal to 8 characters");

            if (!isEmail)
            {
                exceptions.Add("Not a valid email");
            }
            else
            {
                var user = await _userService.GetUserByEmail(inputEmail);
                if (user == null)
                {
                    exceptions.Add("Email does not exist");
                }
                else
                {
                    bool IsValid = VerifyPassword(inputPassword, user.Password, user.Salt);
                    if (IsValid) exceptions.Add("Password does not match with the email");
                }
            }

            if (exceptions.Count != 0) { throw new CustomException(exceptions); };
        }

        public async Task DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) { throw new Exception("For the provided id, user does not exist"); };
            await _userService.DeleteUser(user);
        }

        public async Task<string> CreateOrUpdate(UserDto userDto)
        {
            var exceptions = new List<string>();

            var inputUserName = userDto.UserName.Trim();
            var inputEmail = userDto.Email.Trim();
            var inputPassword = userDto.Password.Trim();

            if (inputUserName == null) { exceptions.Add("User name is required"); }
            if (inputEmail == null) { exceptions.Add("Email is required"); }
            if (inputPassword == null) { exceptions.Add("Password is required"); }

            if (inputUserName.Length < 3) exceptions.Add("User name must be greater than or equal to 3 characters");
            if (inputPassword.Length < 8) exceptions.Add("Password must be greater than or equal to 8 characters");

            bool isEmail = Regex.IsMatch(inputEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail) exceptions.Add("Not a valid email");

            var user = await _userService.GetUserById(userDto.UserId);

            if (user == null)
            {
                var IsEmailExist = await _userService.GetUserByEmail(inputEmail);
                if (IsEmailExist != null) exceptions.Add("Email already exists");

                var IsNameExist = await _userService.GetUserByName(inputUserName);
                if (IsNameExist != null) exceptions.Add("User name already exists");

                if (exceptions.Count > 0) { throw new CustomException(exceptions); }

                var passwordHashAndSalt = _userService.CreatePasswordHash(inputPassword);
                var newUser = new User
                {
                    UserName = inputUserName,
                    Email = inputEmail,
                    Password = passwordHashAndSalt.passwordHash,
                    Salt = passwordHashAndSalt.salt,
                    CreatedBy = "akash"
                };
                await _userService.AddUser(newUser);
                return "User created successfully";
            }
            else
            {
                var UserNameExist = await _userService.GetUserByName(inputUserName);
                if (UserNameExist != null && !(user.UserName == inputUserName)) exceptions.Add("User name already exists");

                var IsEmailExist = await _userService.GetUserByEmail(inputEmail);
                if (IsEmailExist != null && !(user.Email == inputEmail)) exceptions.Add("Email already exists");

                if (exceptions.Count > 0) { throw new CustomException(exceptions); }

                var passwordHashAndSalt = _userService.CreatePasswordHash(inputPassword);
                user.UserName = inputUserName;
                user.Email = inputEmail;
                user.Password = passwordHashAndSalt.passwordHash;
                user.Salt = passwordHashAndSalt.salt;

                await _userService.UpdateUser();
                return "User updated successfully";
            }
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            var result = _userService.VerifyPassword(password, hash, salt);
            return result;
        }
    }
}
