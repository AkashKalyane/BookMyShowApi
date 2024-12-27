using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface IDirectorService
    {
        Task<List<Director>> GetDirectors();
        Task<Director> GetDirectorById(int id);
        Task AddDirector(Director director);
        Task UpdateDirector(Director director);
        Task DeleteDirector(int id);
    }
}
