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
            var theater = await _theaterManager.GetTheaterById(id);
            if (theater == null)
            {
                return NotFound();
            }
            return Ok(theater);
        }

        [HttpPost]
        public async Task<ActionResult> AddTheater(TheaterDto theaterDto)
        {
            if (theaterDto == null)
            {
                return Content("Please provide information");
            }
            await _theaterManager.AddTheater(theaterDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTheater(int id, TheaterDto theaterDto)
        {
            var isExist = await _theaterManager.GetTheaterById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _theaterManager.UpdateTheater(id, theaterDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTheater(int id)
        {
            var theater = await _theaterManager.GetTheaterById(id);
            if (theater == null)
            {
                return NotFound();
            }
            await _theaterManager.DeleteTheater(id);
            return Ok();
        }
    }
}
