using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int GenreId { get; set; }
        public string Grade { get; set; }
        public bool IsAdult { get; set; }   
        public string ReleaseYear  { get; set; }
        public int DirectorId { get; set; }
        public int MainActorMale { get; set; }
        public int MainActorFemale { get; set; }
        public static MovieDto MapToDto(Movie movie) => new MovieDto
        {
            MovieId = movie.MovieId,
            MovieName = movie.MovieName,
            GenreId = movie.GenreId,
            Grade = movie.Grade,
            IsAdult = movie.IsAdult,
            ReleaseYear = movie.ReleaseYear,
            DirectorId = movie.DirectorId,
            MainActorMale = movie.MainActorMale,
            MainActorFemale = movie.MainActorFemale,
        };
    }
}
