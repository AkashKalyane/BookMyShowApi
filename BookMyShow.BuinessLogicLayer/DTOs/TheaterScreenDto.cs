using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class TheaterScreenDto
    {
        public int TheaterScreenId { get; set; }
        public int? TheaterId { get; set; }
        public string ScreenName { get; set; }

        public static TheaterScreenDto MapToDto(TheaterScreen theaterScreen) => new TheaterScreenDto()
        {
            TheaterScreenId = theaterScreen.TheaterScreenId,
            TheaterId = theaterScreen.TheaterId,
            ScreenName = theaterScreen.ScreenName,
        };
    }
}
