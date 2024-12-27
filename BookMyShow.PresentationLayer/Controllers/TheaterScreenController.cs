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
            var theaterScreen = await _theaterScreenManager.GetTheaterScreenById(id);
            if (theaterScreen == null)
            {
                return NotFound();
            }
            return Ok(theaterScreen);
        }

        [HttpPost]
        public async Task<ActionResult> AddTheaterScreen(TheaterScreenDto theaterScreenDto)
        {
            if (theaterScreenDto == null)
            {
                return Content("Please provide information");
            }
            await _theaterScreenManager.AddTheaterScreen(theaterScreenDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTheaterScreen(int id, TheaterScreenDto theaterScreenDto)
        {
            var isExist = await _theaterScreenManager.GetTheaterScreenById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _theaterScreenManager.UpdateTheaterScreen(id, theaterScreenDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTheaterScreen(int id)
        {
            var theaterScreen = await _theaterScreenManager.GetTheaterScreenById(id);
            if (theaterScreen == null)
            {
                return NotFound();
            }
            await _theaterScreenManager.DeleteTheaterScreen(id);
            return Ok();
        }
    }
}
