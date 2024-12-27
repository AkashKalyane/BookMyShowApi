using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.DataContext;
using BookMyShow.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.DataAccessLayer.Services
{
    public class UserDetailService: IUserDetailService
    {
        private readonly BookMyShowContext _context;

        public UserDetailService(BookMyShowContext context) { 
            this._context = context;
        }

        public async Task<List<UserDetail>> GetUserDetails()
        {
            return await _context.UserDetails.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<UserDetail> GetUserDetailById(int id)
        {
            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail.DeletedBy == null) { 
                return userDetail;
            }
            return null;
        }
        public async Task AddUserDetail(UserDetail userDetail)
        {
            await _context.UserDetails.AddAsync(userDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserDetail(UserDetail userDetail)
        {
            _context.Entry(userDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserDetail(int id)
        {
            var userDetail = await _context.UserDetails.FindAsync(id);
            userDetail.DeletedBy = 1;
            userDetail.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
            //if (userDetail != null)
            //{
            //    _context.UserDetails.Remove(userDetail);
            //    await _context.SaveChangesAsync();
            //}
        }
    }
}
