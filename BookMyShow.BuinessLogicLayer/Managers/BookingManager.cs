using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class BookingManager
    {
        private readonly IBookingService _bookingService;

        public BookingManager(IBookingService bookingService) { this._bookingService = bookingService; }

        public async Task<List<BookingDto>> GetBookings()
        {
            var bookings = await _bookingService.GetBookings();
            return bookings.Select(x => BookingDto.MapToDto(x)).ToList();
        }

        public async Task<BookingDto> GetBookingById(int id)
        {
            var result = await _bookingService.GetBookingById(id);
            if (result == null)
            {
                return null;
            }
            return BookingDto.MapToDto(result);
        }

        public async Task AddBooking(BookingDto bookingDto)
        {
            if (bookingDto != null)
            {
                var booking = new Booking
                {
                    MovieId = bookingDto.MovieId,
                    TheaterScreenId = bookingDto.TheaterScreenId,
                    Price = bookingDto.Price,
                    SlotId = bookingDto.SlotId,
                    NumberOfSlot = bookingDto.NumberOfSlot,
                    CreatedBy = 1
                };
                await _bookingService.AddBooking(booking);
            }
        }

        public async Task UpdateBooking(int id, BookingDto bookingDto)
        {
            if (bookingDto != null)
            {
                var booking = await _bookingService.GetBookingById(id);
                booking.MovieId = bookingDto.MovieId;
                booking.TheaterScreenId = bookingDto.TheaterScreenId;
                booking.Price = bookingDto.Price;
                booking.SlotId = bookingDto.SlotId;
                booking.NumberOfSlot = bookingDto.NumberOfSlot;
                booking.ChangedBy = 1;
                booking.ChangedOn = DateTime.Now;
                await _bookingService.UpdateBooking(booking);
            }
        }

        public async Task DeleteBooking(int id)
        {
            await _bookingService.DeleteBooking(id);
        }

    }
}
