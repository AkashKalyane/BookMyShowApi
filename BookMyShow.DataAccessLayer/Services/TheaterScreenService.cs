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
    public class TheaterScreenService: ITheaterScreenService
    {
        private readonly BookMyShowContext _context;

        public TheaterScreenService(BookMyShowContext context) { this._context = context; }

        public async Task<List<TheaterScreen>> GetTheaterScreens()
        {
            return await _context.TheaterScreens.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<TheaterScreen> GetTheaterScreenById(int id)
        {
            var theaterScreen = await _context.TheaterScreens.FindAsync(id);
            if (theaterScreen.DeletedBy == null)
            {
                return theaterScreen;
            }
            return null;
        }

        public async Task AddTheaterScreen(TheaterScreen theaterScreen)
        {
            await _context.TheaterScreens.AddAsync(theaterScreen);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTheaterScreen(TheaterScreen theaterScreen)
        {
            _context.Entry(theaterScreen).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTheaterScreen(int id)
        {
            var theaterScreen = await _context.TheaterScreens.FindAsync(id);
            theaterScreen.DeletedBy = 1;
            theaterScreen.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
