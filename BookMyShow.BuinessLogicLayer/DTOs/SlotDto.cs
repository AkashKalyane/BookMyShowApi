using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class SlotDto
    {
        public int SlotId { get; set; }
        public string SlotName { get; set; }
        public bool IsAvailable { get; set; }

        public static SlotDto MapToDto(Slot slot) => new SlotDto()
        {
            SlotId = slot.SlotId,
            SlotName = slot.SlotName,
            IsAvailable = slot.IsAvailable,
        };
    }
}
