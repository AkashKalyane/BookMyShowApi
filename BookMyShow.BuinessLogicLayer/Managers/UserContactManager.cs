using BookMyShow.BuinessLogicLayer.CustomExceptions;
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
                throw new Exception("User contact does not exist for the provided id");
            }
            return UserContactDto.MapToDto(result);
        }

        public async Task AddUserContact(UserContactDto userContactDto)
        {
            var exceptions = new List<string>();

            var inputPhoneNumber = userContactDto.PhoneNumber.Trim();
            var inputCountryCode = userContactDto.CountryCode.Trim();

            if(!(inputPhoneNumber.Length > 9 && inputPhoneNumber.Length < 11)) { exceptions.Add($"Phone number should be of 10 numbers, current count is {inputPhoneNumber.Length}"); } 
            if(!(inputCountryCode.Length > 1 && inputCountryCode.Length < 3)) { exceptions.Add("Country code should be of 2 characters"); }

            var contacts = await _userContactService.GetUserContacts();
            var test = contacts.Any(x => x.PhoneNumber == inputPhoneNumber);

            //var test = from contact in contacts
            //           where contact.PhoneNumber == inputPhoneNumber
            //           select contact;

            if(test) { exceptions.Add("phone number already exist"); }

            //foreach (var item in contacts)
            //{

            //    if(item.PhoneNumber == inputPhoneNumber)
            //    {
            //        exceptions.Add("Phone number exists");
            //        break;
            //    }
            //}

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

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

        public async Task UpdateUserContact(int id, UserContactDto userContactDto)
        {
            var exceptions = new List<string>();

            var inputPhoneNumber = userContactDto.PhoneNumber.Trim();
            var inputCountryCode = userContactDto.CountryCode.Trim();

            if (!(inputPhoneNumber.Length > 9 && inputPhoneNumber.Length < 11)) { exceptions.Add($"Phone number should be of 10 numbers, current count is {inputPhoneNumber.Length}"); }
            if (!(inputCountryCode.Length > 1 && inputCountryCode.Length < 3)) { exceptions.Add("Country code should be of 2 characters"); }

            var userContact = await _userContactService.GetUserContactById(id);
             if(userContact == null) { exceptions.Add("User contact does not exist for the id"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            userContact.PhoneNumber = inputPhoneNumber;
            userContact.CountryCode = inputCountryCode;
            userContact.IsActive = userContactDto.IsActive;
            userContact.ChangedBy = 1;
            userContact.ChangedOn = DateTime.Now;

            await _userContactService.UpdateUserContact();
        }

        public async Task DeleteUserContact(int id)
        {
            var userContact = await _userContactService.GetUserContactById(id);
            if( userContact == null ) { throw new Exception("User contact does not exist for the provided id"); }

            await _userContactService.DeleteUserContact(id);
        }

    }
}
