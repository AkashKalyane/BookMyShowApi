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
            var actor = await _actorManager.GetActorById(id);
            if (actor == null)
            {
                return NotFound();
            }
            return Ok(actor);
        }

        [HttpPost]
        public async Task<ActionResult> AddActor(ActorDto actorDto)
        {
            if (actorDto == null)
            {
                return Content("Please provide information");
            }
            await _actorManager.AddActor(actorDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateActor(int id, ActorDto actorDto)
        {
            var isExist = await _actorManager.GetActorById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _actorManager.UpdateActor(id, actorDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(int id)
        {
            var actor = await _actorManager.GetActorById(id);
            if (actor == null)
            {
                return NotFound();
            }
            await _actorManager.DeleteActor(id);
            return Ok();
        }
    }
}
