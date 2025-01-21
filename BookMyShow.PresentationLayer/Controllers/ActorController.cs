using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly ActorManager _actorManager;

        public ActorController(ActorManager actorManager) { this._actorManager = actorManager; }

        [HttpGet]
        public async Task<List<ActorDto>> GetActors()
        {
            var actors = await _actorManager.GetActors();
            return actors;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActorById(int id)
        {
            try
            {
                var actor = await _actorManager.GetActorById(id);
                return Ok(actor);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            
        }

        [HttpPost]
        public async Task<ActionResult> AddActor(ActorDto actorDto)
        {
            try
            {
                await _actorManager.AddActor(actorDto);
                return Ok("Actor added successfully");
            } catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateActor(int id, ActorDto actorDto)
        {
            try
            {
                await _actorManager.UpdateActor(id, actorDto);
                return Ok("Actor updated successfully");
            }
            catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(int id)
        {
            try
            {
                await _actorManager.DeleteActor(id);
                return Ok("Actor deleted successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
