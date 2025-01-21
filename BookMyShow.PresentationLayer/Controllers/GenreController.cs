using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreManager _genreManager;

        public GenreController(GenreManager genreManager) { this._genreManager = genreManager; }

        [HttpGet]
        public async Task<List<GenreDto>> GetGenres()
        {
            var genres = await _genreManager.GetGenres();
            return genres;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenreById(int id)
        {
            try
            {
                var genre = await _genreManager.GetGenreById(id);
                return Ok(genre);
            } catch (Exception ex) { return BadRequest(ex.Message); } 
        }

        [HttpPost]
        public async Task<ActionResult> AddGenre(GenreDto genreDto)
        {
            try
            {
                await _genreManager.AddGenre(genreDto);
                return Ok("Genre added successfully");
            } catch(CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateGenre(int id, GenreDto genreDto)
        {
            try
            {
                await _genreManager.UpdateGenre(id, genreDto);
                return Ok("Genre updated successfully");
            } catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            try
            {
                await _genreManager.DeleteGenre(id);
                return Ok("Genre deleted successfully");
            } catch(Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
