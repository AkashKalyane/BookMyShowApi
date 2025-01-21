using BookMyShow.BuinessLogicLayer.CustomExceptions;
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
            try
            {
                var director = await _directorManager.GetDirectorById(id);
                return Ok(director);
            } catch (Exception ex) { return BadRequest(ex.Message); }
            
        }

        [HttpPost]
        public async Task<ActionResult> AddDirector(DirectorDto directorDto)
        {
            try
            {
                await _directorManager.AddDirector(directorDto);
                return Ok("Director added successfully");
            } catch(CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateDirector(int id, DirectorDto directorDto)
        {
            try
            {
                await _directorManager.UpdateDirector(id, directorDto);
                return Ok("Director updated successfully");
            } catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDirector(int id)
        {
            try
            {
                await _directorManager.DeleteDirector(id);
                return Ok("Director deleted successfully");
            }  catch(Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
