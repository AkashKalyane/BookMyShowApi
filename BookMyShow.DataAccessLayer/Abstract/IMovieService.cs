using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();
        Task<Movie> GetMovieById(int id);
        Task AddMovie(Movie movie);
        Task UpdateMovie();
        Task DeleteMovie(int id);
    }
}
