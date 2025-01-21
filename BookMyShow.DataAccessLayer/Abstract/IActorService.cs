using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface IActorService
    {
        Task<List<Actor>> GetActors();
        Task<Actor> GetActorById(int id);
        Task AddActor(Actor actor);
        Task UpdateActor();
        Task DeleteActor(int id);
    }
}
