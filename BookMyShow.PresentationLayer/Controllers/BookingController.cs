using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var booking = await _bookingManager.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult> AddBooking(BookingDto bookingDto)
        {
            if (bookingDto == null)
            {
                return Content("Please provide information");
            }
            await _bookingManager.AddBooking(bookingDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBooking(int id, BookingDto bookingDto)
        {
            var isExist = await _bookingManager.GetBookingById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _bookingManager.UpdateBooking(id, bookingDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            var booking = await _bookingManager.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            await _bookingManager.DeleteBooking(id);
            return Ok();
        }
    }
}
