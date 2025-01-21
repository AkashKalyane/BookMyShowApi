using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.CustomExceptions;
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
                throw new Exception("Genre does not exist for the provided id");
            }
            return GenreDto.MapToDto(result);
        }

        public async Task AddGenre(GenreDto genreDto)
        {
            var exceptions = new List<string>();

            var inputGenreName = genreDto.GenreName.Trim();
            if (inputGenreName.Length <= 3) { exceptions.Add("Genre name should be more than or equal to 3 charaters"); }

            var genreNameIsExist = await _genreService.GetGenreByName(inputGenreName);
            if (genreNameIsExist != null) { exceptions.Add("Genre name already exists"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            var genre = new Genre
            {
                GenreName = inputGenreName,
                CreatedBy = 1
            };

            await _genreService.AddGenre(genre);
        }

        public async Task UpdateGenre(int id, GenreDto genreDto)
        {
            var exceptions = new List<string>();

            var inputGenreName = genreDto.GenreName.Trim();
            if (inputGenreName.Length <= 3) { exceptions.Add("Genre name should be more than or equal to 3 charaters"); }
            var genre = await _genreService.GetGenreById(id);
            if (genre == null) { exceptions.Add("Genre does not exist for the provided id"); }
            else
            {
                var genreNameIsExist = await _genreService.GetGenreByName(inputGenreName);
                if (genreNameIsExist != null) { exceptions.Add("Genre name already exists"); }
            }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            genre.GenreName = genreDto.GenreName;
            genre.ChangedBy = 1;
            genre.ChangedOn = DateTime.Now;

            await _genreService.UpdateGenre();
        }

        public async Task DeleteGenre(int id)
        {
            var genre = await _genreService.GetGenreById(id);
            if (genre == null) { throw new Exception("Genre does not exist for the provided id"); }
            await _genreService.DeleteGenre(id);
        }

    }
}
