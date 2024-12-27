using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.Managers
{
    public class TheaterManager
    {
        private readonly ITheaterService _theaterService;

        public TheaterManager(ITheaterService theaterService) { this._theaterService = theaterService; }

        public async Task<List<TheaterDto>> GetTheaters()
        {
            var theaters = await _theaterService.GetTheaters();
            return theaters.Select(x => TheaterDto.MapToDto(x)).ToList();
        }

        public async Task<TheaterDto> GetTheaterById(int id)
        {
            var result = await _theaterService.GetTheaterById(id);
            if (result == null)
            {
                return null;
            }
            return TheaterDto.MapToDto(result);
        }

        public async Task AddTheater(TheaterDto theaterDto)
        {
            if (theaterDto != null)
            {
                var theater = new Theater
                {
                    TheaterName = theaterDto.TheaterName,
                    IsMultiScreen = theaterDto.IsMultiScreen,
                    CreatedBy = 1
                };
                await _theaterService.AddTheater(theater);
            }
        }

        public async Task UpdateTheater(int id, TheaterDto theaterDto)
        {
            if (theaterDto != null)
            {
                var theater = await _theaterService.GetTheaterById(id);
                theater.TheaterName = theaterDto.TheaterName;
                theater.IsMultiScreen = theaterDto.IsMultiScreen;
                theater.ChangedBy = 1;
                theater.ChangedOn = DateTime.Now;
                await _theaterService.UpdateTheater(theater);
            }
        }

        public async Task DeleteTheater(int id)
        {
            await _theaterService.DeleteTheater(id);
        }

    }
}
