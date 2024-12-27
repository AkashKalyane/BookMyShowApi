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
    public class DirectorService : IDirectorService
    {
        private readonly BookMyShowContext _context;

        public DirectorService(BookMyShowContext context)
        {
            this._context = context;
        }

        public async Task<List<Director>> GetDirectors()
        {
            return await _context.Directors.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<Director> GetDirectorById(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director.DeletedBy == null)
            {
                return director;
            }
            return null;
        }

        public async Task AddDirector(Director director)
        {
            await _context.Directors.AddAsync(director);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDirector(Director director)
        {
            _context.Entry(director).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDirector(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            director.DeletedBy = 1;
            director.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
