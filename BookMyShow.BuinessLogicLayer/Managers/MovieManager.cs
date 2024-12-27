using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;
using BookMyShow.DataAccessLayer.Services;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class MovieManager
    {
        private readonly IMovieService _movieService;

        public MovieManager(IMovieService movieService)
        {
            this._movieService = movieService;
        }
        public async Task<List<MovieDto>> GetMovies()
        {
            var result = await _movieService.GetMovies();
            return result.Select(x => MovieDto.MapToDto(x)).ToList();
        }

        public async Task<MovieDto> GetMovieById(int id)
        {
            var result = await _movieService.GetMovieById(id);
            if (result == null)
            {
                return null;
            }
            return MovieDto.MapToDto(result);
        }

        public async Task AddMovie(MovieDto movieDto)
        {
            if (movieDto != null)
            {
                var movie = new Movie
                {
                    MovieName = movieDto.MovieName,
                    GenreId = movieDto.GenreId,
                    Grade = movieDto.Grade,
                    IsAdult = movieDto.IsAdult,
                    ReleaseYear = movieDto.ReleaseYear,
                    MainActorMale = movieDto.MainActorMale,
                    MainActorFemale = movieDto.MainActorFemale,
                    CreatedBy = 1
                };
                await _movieService.AddMovie(movie);
            }
        }

        public async Task UpdateMovie(int id, MovieDto movieDto)
        {
            if (movieDto != null)
            {
                var movie = await _movieService.GetMovieById(id);
                movie.MovieName = movieDto.MovieName;
                movie.GenreId = movieDto.GenreId;
                movie.Grade = movieDto.Grade;
                movie.IsAdult = movieDto.IsAdult;
                movie.ReleaseYear = movieDto.ReleaseYear;
                movie.MainActorMale = movieDto.MainActorMale;
                movie.MainActorFemale= movieDto.MainActorFemale;
                movie.ChangedBy = 1;
                movie.ChangedOn = DateTime.Now;
                await _movieService.UpdateMovie(movie);
            }
        }

        public async Task DeleteMovie(int id)
        {
            await _movieService.DeleteMovie(id);
        }

    }
}
