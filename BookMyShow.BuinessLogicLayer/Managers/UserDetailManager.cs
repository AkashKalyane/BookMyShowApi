using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;
using BookMyShow.DataAccessLayer.Services;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class UserDetailManager
    {
        private readonly IUserDetailService _userDetailService;

        public UserDetailManager(IUserDetailService userDetailService)
        {
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
                throw new Exception("User doesn't exist for the specified id");
            }
            return UserDetailDto.MapToDto(result);
        }

        public async Task AddUserDetail(UserDetailDtoRequest userDetailDto)
        {
            var exceptions = new List<string>();

            var inputUserId = userDetailDto.UserId;
            var inputFirstName = userDetailDto.FirstName.Trim();
            var inputLastName = userDetailDto.LastName.Trim();
            var inputMiddleName = userDetailDto.MiddleName.Trim();

            if (inputFirstName.Length <= 3) { exceptions.Add("First name should be greater than or equal to 3"); }
            if (inputMiddleName.Length <= 3) { exceptions.Add("Middle name should be greater than or equal to 3"); }
            if (inputLastName.Length <= 3) { exceptions.Add("Last name should be greater than or equal to 3"); }

            var userDetailIsExist = await _userDetailService.GetUserDetailByUserId(inputUserId);
            if (userDetailIsExist != null)
            {
                exceptions.Add("A userDetail already exist in the database with the specified id");
            }
            else
            {
                var user = await _userDetailService.GetUserById(inputUserId);
                if (user == null)
                {
                    exceptions.Add("User does not exist for the specified id");
                }
            }

            if (exceptions.Count > 0) throw new CustomException(exceptions);

            var userDetail = new UserDetail
            {
                UserId = inputUserId,
                FirstName = inputFirstName,
                LastName = inputLastName,
                MiddleName = inputMiddleName,
                CreatedBy = 1
            };
            await _userDetailService.AddUserDetail(userDetail);
        }

        public async Task UpdateUserDetail(int id, UserDetailDtoRequest userDetailDto)
        {
            var exceptions = new List<string>();

            var inputFirstName = userDetailDto.FirstName.Trim();
            var inputLastName = userDetailDto.LastName.Trim();
            var inputMiddleName = userDetailDto.MiddleName.Trim();

            if(inputFirstName.Length <= 3) { exceptions.Add("First name should be greater than or equal to 3"); }
            if(inputMiddleName.Length <= 3) { exceptions.Add("Middle name should be greater than or equal to 3"); }
            if(inputLastName.Length <= 3) { exceptions.Add("Last name should be greater than or equal to 3"); }

            var userDetail = await _userDetailService.GetUserDetailById(id);
            if (userDetail == null) { exceptions.Add("Can not find user details for the specified id"); }

            if(exceptions.Count > 0) throw new CustomException(exceptions);

            userDetail.FirstName = userDetailDto.FirstName;
            userDetail.LastName = userDetailDto.LastName;
            userDetail.MiddleName = userDetailDto.MiddleName;
            userDetail.ChangedBy = 1;
            userDetail.ChangedOn = DateTime.Now;

            await _userDetailService.UpdateUserDetail();
        }

        public async Task DeleteUserDetail(int id)
        {
            var userDetail = await _userDetailService.GetUserDetailById(id);
            if (userDetail == null) { throw new Exception("Can not find user detials for the specified id"); }
            await _userDetailService.DeleteUserDetail(id);
        }

    }
}
