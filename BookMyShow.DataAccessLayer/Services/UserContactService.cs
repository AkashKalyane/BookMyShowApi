﻿using System;
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
    public class UserContactService: IUserContactService
    {
        private readonly BookMyShowContext _context;

        public UserContactService(BookMyShowContext context) {
            this._context = context;
        }

        public async Task<List<UserContact>> GetUserContacts()
        {
            return await _context.UserContacts.Where(x => x.DeletedBy == null).ToListAsync();
        }


        public async Task<UserContact> GetUserContactById(int id)
        {
            var userContact = await _context.UserContacts.FindAsync(id);
            if (userContact.DeletedBy == null)
            {
                return userContact;
            }
            return null;
        }
        public async Task AddUserContact(UserContact userContact)
        {
            await _context.UserContacts.AddAsync(userContact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserContact(UserContact userContact)
        {
            _context.Entry(userContact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserContact(int id)
        {
            var userContact = await _context.UserContacts.FindAsync(id);
            userContact.DeletedBy = 1;
            userContact.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
            //if (userContact != null)
            //{
            //    _context.UserContacts.Remove(userContact);
            //    await _context.SaveChangesAsync();
            //}
        }
    }
}