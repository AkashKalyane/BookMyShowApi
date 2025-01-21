using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.CustomExceptions;
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
                throw new Exception("Actor does not exist for the provided id");
            }
            return ActorDto.MapToDto(result);
        }

        public async Task AddActor(ActorDto actorDto)
        {
            var exceptions = new List<string>();

            var inputActorName = actorDto.ActorName.Trim();
            if(actorDto.Age <= 0) { exceptions.Add("Age must be a positive number"); }
            if(actorDto.HasAward  == null) { exceptions.Add("HasAward is required"); }
            if (actorDto.NoOfMoviesWorkedOn <= 0) { exceptions.Add("Provde number of movies actor has worked on"); }
            if (inputActorName.Length < 6) { exceptions.Add("Actor name should be more than 6 characters"); }

            if(exceptions.Count > 0) { throw new CustomException(exceptions); }

            var actor = new Actor
            {
                Name = inputActorName,
                Age = actorDto.Age,
                HasAward = actorDto.HasAward,
                NoOfMoviesWorkedOn = actorDto.NoOfMoviesWorkedOn,
                CreatedBy = 1
            };
            await _actorService.AddActor(actor);
        }

        public async Task UpdateActor(int id, ActorDto actorDto)
        {
            var exceptions = new List<string>();

            if (actorDto.Age <= 0) { exceptions.Add("Age must be a positive number"); }
            if (actorDto.HasAward == null) { exceptions.Add("HasAward is required"); }
            if (actorDto.NoOfMoviesWorkedOn <= 0) { exceptions.Add("Provde number of movies actor has worked on"); }

            var inputActorName = actorDto.ActorName.Trim();
            if (inputActorName.Length < 6) { exceptions.Add("Actor name should be more than 6 characters"); }

            var actor = await _actorService.GetActorById(id);
            if (actor == null) { exceptions.Add("Actor does not exist for the provided id"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            actor.Name = actorDto.ActorName;
            actor.Age = actorDto.Age;
            actor.HasAward = actorDto.HasAward;
            actor.NoOfMoviesWorkedOn = actorDto.NoOfMoviesWorkedOn;
            actor.ChangedBy = 1;
            actor.ChangedOn = DateTime.Now;
            await _actorService.UpdateActor();
        }

        public async Task DeleteActor(int id)
        {
            var isExist = await _actorService.GetActorById(id);
            if(isExist == null) { throw new Exception("Actor does not exist for the provided id"); }
            await _actorService.DeleteActor(id);
        }

    }
}
