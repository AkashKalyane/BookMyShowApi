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
            var genre = await _genreManager.GetGenreById(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult> AddGenre(GenreDto genreDto)
        {
            if (genreDto == null)
            {
                return Content("Please provide information");
            }
            await _genreManager.AddGenre(genreDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGenre(int id, GenreDto genreDto)
        {
            var isExist = await _genreManager.GetGenreById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _genreManager.UpdateGenre(id, genreDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var genre = await _genreManager.GetGenreById(id);
            if (genre == null)
            {
                return NotFound();
            }
            await _genreManager.DeleteGenre(id);
            return Ok();
        }
    }
}
