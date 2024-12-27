using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<UserDto>? GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            if (result == null)
            {
                return null;
            }
            return UserDto.MapToDto(result);
        }

        public async Task AddUser(UserDto userDto)
        {
            if (userDto != null)
            {
                var user = new User
                {
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    CreatedBy = "akash"
                };
                await _userService.AddUser(user);
            }
        }

        public async Task UpdateUser(int id, UserDto userDto)
        {
            if (userDto != null)
            {
                var user = await _userService.GetUserById(id);
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;
                user.Password = userDto.Password;
                await _userService.UpdateUser(user);
            }
        }

        public async Task DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
        }
    }
}
