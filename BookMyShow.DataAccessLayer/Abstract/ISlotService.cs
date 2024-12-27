using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface ISlotService
    {
        Task<List<Slot>> GetSlots();
        Task<Slot> GetSlotById(int id);
        Task AddSlot(Slot slot);
        Task UpdateSlot(Slot slot);
        Task DeleteSlot(int id);
    }
}
