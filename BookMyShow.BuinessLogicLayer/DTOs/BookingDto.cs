using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int? MovieId { get; set; }
        public int? TheaterScreenId { get; set; }
        public int Price { get; set; }
        public int? SlotId { get; set; }
        public int NumberOfSlot { get; set; }

        public static BookingDto MapToDto(Booking booking) => new BookingDto()
        {
            BookingId = booking.BookingId,
            MovieId = booking.MovieId,
            TheaterScreenId = booking.TheaterScreenId,
            Price = booking.Price,
            SlotId = booking.SlotId,
            NumberOfSlot = booking.NumberOfSlot,
        };
    }
}
