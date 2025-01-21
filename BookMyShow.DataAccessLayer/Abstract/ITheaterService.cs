using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface ITheaterService
    {
        Task<List<Theater>> GetTheaters();
        Task<Theater> GetTheaterById(int id);
        Task<Theater> GetTheaterByName(string name);
        Task AddTheater(Theater theater);
        Task UpdateTheater();
        Task DeleteTheater(int id);
    }
}
