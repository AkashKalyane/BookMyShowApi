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
            var movie = await _movieManager.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie(MovieDto movieDto)
        {
            if (movieDto == null)
            {
                return Content("Please provide information");
            }
            await _movieManager.AddMovie(movieDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, MovieDto movieDto)
        {
            var isExist = await _movieManager.GetMovieById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _movieManager.UpdateMovie(id, movieDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await _movieManager.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            await _movieManager.DeleteMovie(id);
            return Ok();
        }
    }
}
