using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class UserDetailDto
    {
        public int UserDetailId { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullNameFL { get; set; }
        public string FullNameLF { get; set; }
        public string FullNameFML { get; set; }

        public static UserDetailDto MapToDto(UserDetail userDetail) => new UserDetailDto
        {
            UserDetailId = userDetail.UserDetailId,
            UserId = userDetail.UserId,
            FirstName = userDetail.FirstName,
            LastName = userDetail.LastName,
            MiddleName = userDetail.MiddleName,
            FullNameFL = userDetail.FullNameFl,
            FullNameLF = userDetail.FullNameLf,
            FullNameFML = userDetail.FullNameFml,
        };
    }
}
