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
    public class ActorService: IActorService
    {
        private readonly BookMyShowContext _context;

        public ActorService(BookMyShowContext context) { this._context = context; }

        public async Task<List<Actor>> GetActors()
        {
            return await _context.Actors.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<Actor> GetActorById(int id)
        {
            var actor = await _context.Actors.Where(x => x.ActorId == id && x.DeletedBy == null).FirstOrDefaultAsync();
            return actor;
        }

        public async Task AddActor(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActor()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            actor.DeletedBy = 1;
            actor.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
