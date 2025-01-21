using System;
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
    public class BookingService: IBookingService
    {
        private readonly BookMyShowContext _context;

        public BookingService(BookMyShowContext context) { this._context = context; }

        public async Task<List<Booking>> GetBookings()
        {
            return await _context.Bookings.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<Booking> GetBookingById(int id)
        {
            var booking = await _context.Bookings.Where(x => x.BookingId == id && x.DeletedBy == null).FirstOrDefaultAsync();
            return booking;
        }

        public async Task AddBooking(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBooking()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            booking.DeletedBy = 1;
            booking.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
