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
    public class SlotService: ISlotService
    {
        private readonly BookMyShowContext _context;

        public SlotService(BookMyShowContext context) { this._context = context; }

        public async Task<List<Slot>> GetSlots()
        {
            return await _context.Slots.Where(x => x.DeletedBy == null).ToListAsync();
        }

        public async Task<Slot> GetSlotById(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            if (slot.DeletedBy == null)
            {
                return slot;
            }
            return null;
        }

        public async Task AddSlot(Slot slot)
        {
            await _context.Slots.AddAsync(slot);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSlot(Slot slot)
        {
            _context.Entry(slot).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSlot(int id)
        {
            var slot = await _context.Slots.FindAsync(id);
            slot.DeletedBy = 1;
            slot.DeletedOn = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
