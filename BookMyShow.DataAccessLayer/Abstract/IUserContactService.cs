using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface IUserContactService
    {
        Task<List<UserContact>> GetUserContacts();
        Task<UserContact> GetUserContactById(int id);
        Task AddUserContact(UserContact userContact);
        Task UpdateUserContact();
        Task DeleteUserContact(int id);
    }
}
