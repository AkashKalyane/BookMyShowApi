using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class UserContactManager
    {
        private readonly IUserContactService _userContactService;

        public UserContactManager(IUserContactService userContectService) 
        {
            this._userContactService = userContectService;
        }

        public async Task<List<UserContactDto>> GetUserContacts()
        {
            var userContacts = await _userContactService.GetUserContacts();
            return userContacts.Select(x => UserContactDto.MapToDto(x)).ToList();
        }

        public async Task<UserContactDto> GetUserContactById(int id)
        {
       
            var result = await _userContactService.GetUserContactById(id);
            if (result == null)
            {
                return null;
            }
            return UserContactDto.MapToDto(result);
        }

        public async Task AddUserContact(UserContactDto userContactDto)
        {
            if (userContactDto != null)
            {
                var userContact = new UserContact
                {
                    UserId = userContactDto.UserId,
                    PhoneNumber = userContactDto.PhoneNumber,
                    CountryCode = userContactDto.CountryCode,
                    IsActive = userContactDto.IsActive,
                    CreatedBy = 1
                };
                await _userContactService.AddUserContact(userContact);
            }
        }

        public async Task UpdateUserContact(int id, UserContactDto userContactDto)
        {
            if (userContactDto != null)
            {
                var userContact = await _userContactService.GetUserContactById(id);
                userContact.PhoneNumber = userContactDto.PhoneNumber;
                userContact.IsActive = userContactDto.IsActive;
                userContact.ChangedBy = 1;
                userContact.ChangedOn = DateTime.Now;
                await _userContactService.UpdateUserContact(userContact);
            }
        }

        public async Task DeleteUserContact(int id)
        {
            await _userContactService.DeleteUserContact(id);
        }

    }
}
