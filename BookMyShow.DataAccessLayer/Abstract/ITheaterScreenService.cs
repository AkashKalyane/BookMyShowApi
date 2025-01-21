using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.DataAccessLayer.Abstract
{
    public interface ITheaterScreenService
    {
        Task<List<TheaterScreen>> GetTheaterScreens();
        Task<TheaterScreen> GetTheaterScreenById(int id);
        Task AddTheaterScreen(TheaterScreen theaterScreen);
        Task UpdateTheaterScreen();
        Task DeleteTheaterScreen(int id);
        Task<List<string>> verifydata(int? theaterId, string theaterName);
    }
}
