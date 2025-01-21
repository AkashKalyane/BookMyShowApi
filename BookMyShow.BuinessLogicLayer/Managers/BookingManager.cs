using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using BookMyShow.BuinessLogicLayer.CustomExceptions;
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

            var temp = bookings.GroupBy(x => x.MovieId).
                Select(y => new { y.Key, count = y.ToList() }).ToList();

            return bookings.Select(x => BookingDto.MapToDto(x)).ToList();
        }

        public async Task<BookingDto> GetBookingById(int id)
        {
            var result = await _bookingService.GetBookingById(id);
            if (result == null)
            {
                throw new Exception("Booking id does not exist for the provided id");
            }
            return BookingDto.MapToDto(result);
        }

        public async Task AddBooking(BookingDto bookingDto)
        {
            var exceptions = new List<string>();

            if(bookingDto.MovieId == null) { exceptions.Add("MovieId is required"); }
            if(bookingDto.TheaterScreenId == null) { exceptions.Add("TheaterScreenId is required"); }
            if(bookingDto.Price < 100) {  exceptions.Add("Price should be more than 100 rupees"); }
            if(bookingDto.SlotId == null) { exceptions.Add("SlotId is required"); }
            if(bookingDto.NumberOfSlot <= 0) { exceptions.Add("Number of slot should not be zero"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

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

        public async Task UpdateBooking(int id, BookingDto bookingDto)
        {
            var exceptions = new List<string>();

            var inputMovieId = bookingDto.MovieId;
            var inputTheaterScreenId = bookingDto.TheaterScreenId;
            var inputPrice = bookingDto.Price;
            var inputSlotId = bookingDto.SlotId;
            var inputNumberOfSlot = bookingDto.NumberOfSlot;

            if (inputMovieId == null) { exceptions.Add("MovieId is required"); }
            if (inputTheaterScreenId == null) { exceptions.Add("TheaterScreenId is required"); }
            if (inputPrice < 100) { exceptions.Add("Price should be more than 100 rupees"); }
            if (inputSlotId == null) { exceptions.Add("SlotId is required"); }
            if (inputNumberOfSlot <= 0) { exceptions.Add("Number of slot should not be zero"); }

            var booking = await _bookingService.GetBookingById(id);
            if(booking == null ) { exceptions.Add("Booking id does not exist"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            booking.TheaterScreenId = bookingDto.TheaterScreenId;
            booking.Price = bookingDto.Price;
            booking.SlotId = bookingDto.SlotId;
            booking.NumberOfSlot = bookingDto.NumberOfSlot;
            booking.ChangedBy = 1;
            booking.ChangedOn = DateTime.Now;

            await _bookingService.UpdateBooking();
        }

        public async Task DeleteBooking(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null) { throw new Exception("Booking id does not exist"); }
            await _bookingService.DeleteBooking(id);
        }
    }
}
