using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        private readonly TheaterManager _theaterManager;

        public TheaterController(TheaterManager theaterManager) { this._theaterManager = theaterManager; }

        [HttpGet]
        public async Task<List<TheaterDto>> GetTheaters()
        {
            var theaters = await _theaterManager.GetTheaters();
            return theaters;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TheaterDto>> GetTheaterById(int id)
        {
            try
            {
                var theater = await _theaterManager.GetTheaterById(id);
                return Ok(theater);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> AddTheater(TheaterDto theaterDto)
        {
            try
            {
                await _theaterManager.AddTheater(theaterDto);
                return Ok("Theater added successfully");
            } catch(CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateTheater(int id, TheaterDto theaterDto)
        {
            try
            {
                await _theaterManager.UpdateTheater(id, theaterDto);
                return Ok("Theater updated successfully");
            } catch(CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTheater(int id)
        {
            try
            {
                await _theaterManager.DeleteTheater(id);
                return Ok("Theater deleted successfully");
            } catch(Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
