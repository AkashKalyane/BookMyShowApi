using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class TheaterDto
    {
        public int TheaterId { get; set; }
        public string TheaterName { get; set; }
        public bool IsMultiScreen { get; set; }

        public static TheaterDto MapToDto(Theater theater) => new TheaterDto()
        {
            TheaterId = theater.TheaterId,
            TheaterName = theater.TheaterName,
            IsMultiScreen = theater.IsMultiScreen,
        };
    }
}
