using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly DirectorManager _directorManager;

        public DirectorController(DirectorManager directorManager) {
            this._directorManager = directorManager;
        }

        [HttpGet]
        public async Task<List<DirectorDto>> GetDirectors()
        {
            var directors = await _directorManager.GetDirectors();
            return directors;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDto>> GetDirectorById(int id)
        {
            var director = await _directorManager.GetDirectorById(id);
            if (director == null)
            {
                return NotFound();
            }
            return Ok(director);
        }

        [HttpPost]
        public async Task<ActionResult> AddDirector(DirectorDto directorDto)
        {
            if (directorDto == null)
            {
                return Content("Please provide information");
            }
            await _directorManager.AddDirector(directorDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDirector(int id, DirectorDto directorDto)
        {
            var isExist = await _directorManager.GetDirectorById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _directorManager.UpdateDirector(id, directorDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDirector(int id)
        {
            var director = await _directorManager.GetDirectorById(id);
            if (director == null)
            {
                return NotFound();
            }
            await _directorManager.DeleteDirector(id);
            return Ok();
        }
    }
}
