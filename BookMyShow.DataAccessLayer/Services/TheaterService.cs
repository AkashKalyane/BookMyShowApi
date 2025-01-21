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
    public class TheaterService: ITheaterService
    {
        private readonly BookMyShowContext _context;

        public TheaterService(BookMyShowContext context) { this._context = context; }

        public async Task<List<Theater>> GetTheaters()
        {
            return await _context.Theaters.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<Theater> GetTheaterById(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater != null)
            {
                if(theater.DeletedBy == null)
                {
                    return theater;
                }
            }
            return null;
        }

        public async Task<Theater> GetTheaterByName(string name)
        {
            var theater = await _context.Theaters.Where(x => x.TheaterName == name).FirstOrDefaultAsync();
            return theater;
        }

        public async Task AddTheater(Theater theater)
        {
            await _context.Theaters.AddAsync(theater);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTheater()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            theater.DeletedBy = 1;
            theater.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
