using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface IGenreService
    {
        Task<List<Genre>> GetGenres();
        Task<Genre> GetGenreById(int id);
        Task<Genre> GetGenreByName(string genreName);
        Task AddGenre(Genre genre);
        Task UpdateGenre();
        Task DeleteGenre(int id);
    }
}
