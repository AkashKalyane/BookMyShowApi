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
            return await _context.Genres.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<Genre> GetGenreById(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre.DeletedBy == null)
            {
                return genre;
            }
            return null;
        }

        public async Task AddGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenre(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
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
