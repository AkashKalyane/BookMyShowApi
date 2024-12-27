using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class GenreManager
    {
        private readonly IGenreService _genreService;

        public GenreManager(IGenreService genreService) { this._genreService = genreService; }

        public async Task<List<GenreDto>> GetGenres()
        {
            var genres = await _genreService.GetGenres();
            return genres.Select(x => GenreDto.MapToDto(x)).ToList();
        }

        public async Task<GenreDto> GetGenreById(int id)
        {
            var result = await _genreService.GetGenreById(id);
            if (result == null)
            {
                return null;
            }
            return GenreDto.MapToDto(result);
        }

        public async Task AddGenre(GenreDto genreDto)
        {
            if (genreDto != null)
            {
                var genre = new Genre
                {
                    GenreName = genreDto.GenreName,
                    CreatedBy = 1
                };
                await _genreService.AddGenre(genre);
            }
        }

        public async Task UpdateGenre(int id, GenreDto genreDto)
        {
            if (genreDto != null)
            {
                var genre = await _genreService.GetGenreById(id);
                genre.GenreName = genreDto.GenreName;
                genre.ChangedBy = 1;
                genre.ChangedOn = DateTime.Now;
                await _genreService.UpdateGenre(genre);
            }
        }

        public async Task DeleteGenre(int id)
        {
            await _genreService.DeleteGenre(id);
        }

    }
}
