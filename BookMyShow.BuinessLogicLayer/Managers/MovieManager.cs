using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.CustomExceptions;
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
                throw new Exception("Movie does not exist for the provided id");
            }
            return MovieDto.MapToDto(result);
        }

        public async Task AddMovie(MovieDto movieDto)
        {
            var exceptions = new List<string>();

            var inputMovieName = movieDto.MovieName.Trim();
            var inputGrade = movieDto.Grade.Trim();
            if (inputMovieName.Length <= 5) { exceptions.Add("Movie number should be more than 5 charaters"); }
            if (inputGrade.Length > 3) { exceptions.Add("Grade should less than or equal to 3 charaters"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            var movie = new Movie
            {
                MovieName = inputMovieName,
                GenreId = movieDto.GenreId,
                Grade = inputGrade,
                IsAdult = movieDto.IsAdult,
                DirectorId = movieDto.DirectorId,
                ReleaseYear = movieDto.ReleaseYear,
                MainActorMale = movieDto.MainActorMale,
                MainActorFemale = movieDto.MainActorFemale,
                CreatedBy = 1
            };
            await _movieService.AddMovie(movie);
        }

        public async Task UpdateMovie(int id, MovieDto movieDto)
        {
            var exceptions = new List<string>();

            var inputMovieName = movieDto.MovieName.Trim();
            var inputGrade = movieDto.Grade.Trim();
            if (inputMovieName.Length <= 5) { exceptions.Add("Movie name should be more than 5 charaters"); }
            if (inputGrade.Length > 3) { exceptions.Add("Grade should less than or equal to 3 charaters"); }

            var movie = await _movieService.GetMovieById(id);
            if (movie == null) { exceptions.Add("Movie does not exist for the provided id"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            movie.MovieName = inputMovieName;
            movie.GenreId = movieDto.GenreId;
            movie.Grade = inputGrade;
            movie.IsAdult = movieDto.IsAdult;
            movie.DirectorId = movieDto.DirectorId;
            movie.ReleaseYear = movieDto.ReleaseYear;
            movie.MainActorMale = movieDto.MainActorMale;
            movie.MainActorFemale = movieDto.MainActorFemale;
            movie.ChangedBy = 1;
            movie.ChangedOn = DateTime.Now;
            
            await _movieService.UpdateMovie();
        }

        public async Task DeleteMovie(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if(movie == null) { throw new Exception("Movie does not exist for provided id"); }
            await _movieService.DeleteMovie(id);
        }

    }
}
