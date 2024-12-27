using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class SlotManager
    {
        private readonly ISlotService _slotService;

        public SlotManager(ISlotService slotService) { this._slotService = slotService; }

        public async Task<List<SlotDto>> GetSlots()
        {
            var slots = await _slotService.GetSlots();
            return slots.Select(x => SlotDto.MapToDto(x)).ToList();
        }

        public async Task<SlotDto> GetSlotById(int id)
        {
            var result = await _slotService.GetSlotById(id);
            if (result == null)
            {
                return null;
            }
            return SlotDto.MapToDto(result);
        }

        public async Task AddSlot(SlotDto slotDto)
        {
            if (slotDto != null)
            {
                var slot = new Slot
                {
                    SlotName = slotDto.SlotName,
                    IsAvailable = slotDto.IsAvailable,
                    CreatedBy = 1
                };
                await _slotService.AddSlot(slot);
            }
        }

        public async Task UpdateSlot(int id, SlotDto slotDto)
        {
            if (slotDto != null)
            {
                var slot = await _slotService.GetSlotById(id);
                slot.SlotName = slotDto.SlotName;
                slot.IsAvailable = slotDto.IsAvailable;
                slot.ChangedBy = 1;
                slot.ChangedOn = DateTime.Now;
                await _slotService.UpdateSlot(slot);
            }
        }

        public async Task DeleteSlot(int id)
        {
            await _slotService.DeleteSlot(id);
        }

    }
}
