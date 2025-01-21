using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieManager _movieManager;

        public MovieController(MovieManager movieManager)
        {
            this._movieManager = movieManager;
        }
        [HttpGet]
        public async Task<List<MovieDto>> GetMovies()
        {
            var movies = await _movieManager.GetMovies();
            return movies;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovieById(int id)
        {
            try
            {
                var movie = await _movieManager.GetMovieById(id);
                return Ok(movie);
            }  catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie(MovieDto movieDto)
        {
            try
            {
                await _movieManager.AddMovie(movieDto);
                return Ok("Movie added successfully");
            } catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, MovieDto movieDto)
        {
            try
            {
                await _movieManager.UpdateMovie(id, movieDto);
                return Ok("Movie updated successfully");
            }catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieManager.DeleteMovie(id);
                return Ok("Movie deleted successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
