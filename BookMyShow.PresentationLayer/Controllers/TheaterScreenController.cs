using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterScreenController : ControllerBase
    {
        private readonly TheaterScreenManager _theaterScreenManager;

        public TheaterScreenController(TheaterScreenManager theaterScreenManager) { this._theaterScreenManager = theaterScreenManager; }

        [HttpGet]
        public async Task<List<TheaterScreenDto>> GetTheaterScreens()
        {
            var theaterScreens = await _theaterScreenManager.GetTheaterScreens();
            return theaterScreens;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TheaterScreenDto>> GetTheaterScreenById(int id)
        {
            try
            {
                var theaterScreen = await _theaterScreenManager.GetTheaterScreenById(id);
                return Ok(theaterScreen);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> AddTheaterScreen(TheaterScreenDto theaterScreenDto)
        {
            try
            {
                await _theaterScreenManager.AddTheaterScreen(theaterScreenDto);
                return Ok("Theater Screen added successfully");
            } catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateTheaterScreen(int id, TheaterScreenDto theaterScreenDto)
        {
            try
            {
                await _theaterScreenManager.UpdateTheaterScreen(id, theaterScreenDto);
                return Ok("Theater screen updated successfully");
            } catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTheaterScreen(int id)
        {
            try
            {
                await _theaterScreenManager.DeleteTheaterScreen(id);
                return Ok("Theater screen deleted successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
