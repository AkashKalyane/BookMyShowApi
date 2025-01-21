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
            var theaterScreen = await _context.TheaterScreens.Where(x => x.TheaterScreenId == id && x.DeletedBy == null).FirstAsync();
            if (theaterScreen != null)
            {
                if (theaterScreen.DeletedBy == null)
                {
                    return theaterScreen;
                }
            }
            return null;
        }

        public async Task AddTheaterScreen(TheaterScreen theaterScreen)
        {
            await _context.TheaterScreens.AddAsync(theaterScreen);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTheaterScreen()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTheaterScreen(int id)
        {
            var theaterScreen = await _context.TheaterScreens.FindAsync(id);
            theaterScreen.DeletedBy = 1;
            theaterScreen.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> verifydata(int? theaterId, string screenName)
        {
            var exceptions = new List<string>();
            var theater = await _context.Theaters.Where(x => x.TheaterId == theaterId).FirstOrDefaultAsync();
            if(theater == null) { exceptions.Add("theater does not exists for the provided id"); }
            else
            {
                var IsScreenExist = await _context.TheaterScreens.
                Where(x => x.TheaterId == theaterId).
                Select(b => b.ScreenName == screenName).
                FirstOrDefaultAsync();
                if (IsScreenExist) { exceptions.Add("A screen already exist for provided id"); }
            }

            if (exceptions.Count > 0) { return exceptions; }

            return null;
        }
    }
}
