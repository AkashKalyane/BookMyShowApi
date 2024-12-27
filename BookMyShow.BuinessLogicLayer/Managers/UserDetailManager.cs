using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;
using BookMyShow.DataAccessLayer.Services;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class UserDetailManager
    {
        private readonly IUserDetailService _userDetailService;

        public UserDetailManager(IUserDetailService userDetailService) {
            this._userDetailService = userDetailService;
        }

        public async Task<List<UserDetailDto>> GetUserDetails()
        {
            var userDetails = await _userDetailService.GetUserDetails();
            return userDetails.Select(x => UserDetailDto.MapToDto(x)).ToList();
        }

        public async Task<UserDetailDto> GetUserDetailById(int id)
        {

            var result = await _userDetailService.GetUserDetailById(id);
            if (result == null)
            {
                return null;
            }
            return UserDetailDto.MapToDto(result);
        }

        public async Task AddUserDetail(UserDetailDto userDetailDto)
        {
            if (userDetailDto != null)
            {
                var userDetail = new UserDetail
                {
                    UserId = userDetailDto.UserId,
                    FirstName = userDetailDto.FirstName,
                    LastName = userDetailDto.LastName,
                    MiddleName = userDetailDto.MiddleName,
                    CreatedBy = 1
                };
                await _userDetailService.AddUserDetail(userDetail);
            }
        }

        public async Task UpdateUserDetail(int id, UserDetailDto userDetailDto)
        {
            if (userDetailDto != null)
            {
                var userDetail = await _userDetailService.GetUserDetailById(id);
                userDetail.FirstName = userDetailDto.FirstName;
                userDetail.LastName = userDetailDto.LastName;
                userDetail.MiddleName = userDetailDto.MiddleName;
                userDetail.ChangedBy = 1;
                userDetail.ChangedOn = DateTime.Now;
                await _userDetailService.UpdateUserDetail(userDetail);
            }
        }

        public async Task DeleteUserDetail(int id)
        {
            await _userDetailService.DeleteUserDetail(id);
        }

    }
}
