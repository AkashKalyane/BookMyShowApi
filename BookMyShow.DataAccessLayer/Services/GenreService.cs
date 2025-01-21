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
    public class GenreService: IGenreService
    {
        private readonly BookMyShowContext _context;

        public GenreService(BookMyShowContext context) { this._context = context; }

        public async Task<List<Genre>> GetGenres()
        {
            var genres = await _context.Genres.Select(x => new Genre()
            {
                GenreId = x.GenreId,
                Movies = x.Movies.Select(x => new Movie()
                {
                    MovieName = x.MovieName
                }).ToList()
            }).ToListAsync();

            return await _context.Genres.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<Genre> GetGenreById(int id)
        {
            var genre = await _context.Genres.Where(x => x.GenreId == id && x.DeletedBy == null).FirstOrDefaultAsync();
            return genre;
        }

        public async Task<Genre> GetGenreByName(string genreName)
        {
            var genre = await _context.Genres.Where(x => x.GenreName == genreName).FirstOrDefaultAsync();
            return genre;
        }

        public async Task AddGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenre()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            genre.DeletedBy = 1;
            genre.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
