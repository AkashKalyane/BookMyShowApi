using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.DataContext;
using BookMyShow.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.DataAccessLayer.Services
{
    public class MovieService: IMovieService
    {
        private readonly BookMyShowContext _context;
        public MovieService(BookMyShowContext context)
        {
            this._context = context;
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _context.Movies.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie.DeletedBy == null)
            {
                return movie;
            }
            return null;
        }

        public async Task AddMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovie(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            movie.DeletedBy = 1;
            movie.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
