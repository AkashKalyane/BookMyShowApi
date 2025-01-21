using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface IBookingService
    {
        Task<List<Booking>> GetBookings();
        Task<Booking> GetBookingById(int id);
        Task AddBooking(Booking booking);
        Task UpdateBooking();
        Task DeleteBooking(int id);
    }
}
