using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingManager _bookingManager;

        public BookingController(BookingManager bookingManager) { this._bookingManager = bookingManager; }

        [HttpGet]
        public async Task<List<BookingDto>> GetBookings()
        {
            var bookings = await _bookingManager.GetBookings();
            return bookings;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int id)
        {
            try
            {
                var booking = await _bookingManager.GetBookingById(id);
                return Ok(booking);
            } 
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> AddBooking(BookingDto bookingDto)
        {
            try
            {
                await _bookingManager.AddBooking(bookingDto);
                return Ok("Booking added successfully");
            }
            catch (CustomException ex) {
                return BadRequest(ex.list); 
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateBooking(int id, BookingDto bookingDto)
        {
            try
            {
                await _bookingManager.UpdateBooking(id, bookingDto);
                return Ok("Booking updated successfully");
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.list);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            try
            {
                await _bookingManager.DeleteBooking(id);
                return Ok("Booking deleted successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
            
        }
    }
}
