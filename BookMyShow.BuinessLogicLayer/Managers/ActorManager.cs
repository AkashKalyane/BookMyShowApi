using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;
using BookMyShow.DataAccessLayer.Services;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class ActorManager
    {
        private readonly IActorService _actorService;

        public ActorManager(IActorService actorService) { this._actorService = actorService; }

        public async Task<List<ActorDto>> GetActors()
        {
            var actors = await _actorService.GetActors();
            return actors.Select(x => ActorDto.MapToDto(x)).ToList();
        }

        public async Task<ActorDto> GetActorById(int id)
        {
            var result = await _actorService.GetActorById(id);
            if (result == null)
            {
                return null;
            }
            return ActorDto.MapToDto(result);
        }

        public async Task AddActor(ActorDto actorDto)
        {
            if (actorDto != null)
            {
                var actor = new Actor
                {
                    Name = actorDto.ActorName,
                    Age = actorDto.Age,
                    HasAward = actorDto.HasAward,
                    NoOfMoviesWorkedOn = actorDto.NoOfMoviesWorkedOn,
                    CreatedBy = 1
                };
                await _actorService.AddActor(actor);
            }
        }

        public async Task UpdateActor(int id, ActorDto actorDto)
        {
            if (actorDto != null)
            {
                var actor = await _actorService.GetActorById(id);
                actor.Name = actorDto.ActorName;
                actor.Age = actorDto.Age;
                actor.HasAward = actorDto.HasAward;
                actor.NoOfMoviesWorkedOn = actorDto.NoOfMoviesWorkedOn;
                actor.ChangedBy = 1;
                actor.ChangedOn = DateTime.Now;
                await _actorService.UpdateActor(actor);
            }
        }

        public async Task DeleteActor(int id)
        {
            await _actorService.DeleteActor(id);
        }

    }
}
