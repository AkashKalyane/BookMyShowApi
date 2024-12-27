using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class ActorDto
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public int Age { get; set; }
        public bool HasAward { get; set; }
        public int NoOfMoviesWorkedOn { get; set; }

        public static ActorDto MapToDto(Actor actor) => new ActorDto
        {
            ActorId = actor.ActorId,
            ActorName = actor.Name,
            Age = actor.Age,
            HasAward = actor.HasAward,
            NoOfMoviesWorkedOn = actor.NoOfMoviesWorkedOn,
        };
    }
}
