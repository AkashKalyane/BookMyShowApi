using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class UserContactDto
    {
        public int UserContactId { get; set; }
        public int? UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public bool IsActive { get; set; }

        public static UserContactDto MapToDto(UserContact userContact) => new UserContactDto()
        {
            UserContactId = userContact.UserContactId,
            UserId = userContact.UserId,
            PhoneNumber = userContact.PhoneNumber,
            CountryCode = userContact.CountryCode,
            IsActive = userContact.IsActive,
        };
    }
}
