using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface IUserDetailService
    {
        Task<List<UserDetail>> GetUserDetails();
        Task<User> GetUserById(int id);
        Task<UserDetail> GetUserDetailById(int id);
        Task<UserDetail> GetUserDetailByUserId(int? id);
        Task AddUserDetail(UserDetail userDetail);
        Task UpdateUserDetail();
        Task DeleteUserDetail(int id);
    }
}
