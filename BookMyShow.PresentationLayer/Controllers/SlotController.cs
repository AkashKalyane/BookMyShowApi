using BookMyShow.BuinessLogicLayer.CustomExceptions;
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
            try
            {
                var slot = await _slotManager.GetSlotById(id);
                return Ok(slot);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> AddSlot(SlotDto slotDto)
        {
            try
            {
                await _slotManager.AddSlot(slotDto);
                return Ok("Slot added successfully");
            } catch(CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateSlot(int id, SlotDto slotDto)
        {
            try
            {
                await _slotManager.UpdateSlot(id, slotDto);
                return Ok("Slot updated successfully");
            } catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSlot(int id)
        {
            try
            {
                await _slotManager.DeleteSlot(id);
                return Ok("Slot deleted successfuly");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
