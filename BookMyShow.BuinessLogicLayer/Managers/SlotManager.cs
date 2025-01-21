using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.CustomExceptions;
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
                throw new Exception("Can not find slot for provided id");
            }
            return SlotDto.MapToDto(result);
        }

        public async Task AddSlot(SlotDto slotDto)
        {
            var exceptions = new List<string>();

            var inputSlotName = slotDto.SlotName.Trim();
            if(inputSlotName.Length < 5) { exceptions.Add("Slot name should be more than 5 characters"); }

            if(exceptions.Count > 0) { throw new CustomException(exceptions); }

            var slot = new Slot
            {
                SlotName = inputSlotName,
                IsAvailable = slotDto.IsAvailable,
                CreatedBy = 1
            };
            await _slotService.AddSlot(slot);
        }

        public async Task UpdateSlot(int id, SlotDto slotDto)
        {
            var exceptions = new List<string>();

            var inputSlotName = slotDto.SlotName.Trim();
            if (inputSlotName.Length < 5) { exceptions.Add("Slot name should be more than 5 characters"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            if (slotDto != null)
            {
                var slot = await _slotService.GetSlotById(id);
                slot.SlotName = inputSlotName;
                slot.IsAvailable = slotDto.IsAvailable;
                slot.ChangedBy = 1;
                slot.ChangedOn = DateTime.Now;
                await _slotService.UpdateSlot();
            }
        }

        public async Task DeleteSlot(int id)
        {
            var slot = await _slotService.GetSlotById(id);
            if(slot == null) { throw new Exception("Slot does not exist for the provided id"); }
            await _slotService.DeleteSlot(id);
        }

    }
}
