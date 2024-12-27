using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly SlotManager _slotManager;

        public SlotController(SlotManager slotManager) { this._slotManager = slotManager; }

        [HttpGet]
        public async Task<List<SlotDto>> GetSlots()
        {
            var slots = await _slotManager.GetSlots();
            return slots;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SlotDto>> GetSlotById(int id)
        {
            var slot = await _slotManager.GetSlotById(id);
            if (slot == null)
            {
                return NotFound();
            }
            return Ok(slot);
        }

        [HttpPost]
        public async Task<ActionResult> AddSlot(SlotDto slotDto)
        {
            if (slotDto == null)
            {
                return Content("Please provide information");
            }
            await _slotManager.AddSlot(slotDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSlot(int id, SlotDto slotDto)
        {
            var isExist = await _slotManager.GetSlotById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _slotManager.UpdateSlot(id, slotDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSlot(int id)
        {
            var slot = await _slotManager.GetSlotById(id);
            if (slot == null)
            {
                return NotFound();
            }
            await _slotManager.DeleteSlot(id);
            return Ok();
        }
    }
}
