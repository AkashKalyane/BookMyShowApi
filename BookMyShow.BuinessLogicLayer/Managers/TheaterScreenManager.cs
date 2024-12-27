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
    public class TheaterScreenManager
    {
        private readonly ITheaterScreenService _theaterScreenService;

        public TheaterScreenManager(ITheaterScreenService theaterScreenService) { this._theaterScreenService = theaterScreenService; }

        public async Task<List<TheaterScreenDto>> GetTheaterScreens()
        {
            var theaterScreens = await _theaterScreenService.GetTheaterScreens();
            return theaterScreens.Select(x => TheaterScreenDto.MapToDto(x)).ToList();
        }

        public async Task<TheaterScreenDto> GetTheaterScreenById(int id)
        {
            var result = await _theaterScreenService.GetTheaterScreenById(id);
            if (result == null)
            {
                return null;
            }
            return TheaterScreenDto.MapToDto(result);
        }

        public async Task AddTheaterScreen(TheaterScreenDto theaterScreenDto)
        {
            if (theaterScreenDto != null)
            {
                var theaterScreen = new TheaterScreen
                {
                    TheaterId = theaterScreenDto.TheaterId,
                    ScreenName = theaterScreenDto.ScreenName,
                    CreatedBy = 1
                };
                await _theaterScreenService.AddTheaterScreen(theaterScreen);
            }
        }

        public async Task UpdateTheaterScreen(int id, TheaterScreenDto theaterScreenDto)
        {
            if (theaterScreenDto != null)
            {
                var theaterScreen = await _theaterScreenService.GetTheaterScreenById(id);
                theaterScreen.TheaterId = theaterScreenDto.TheaterId;
                theaterScreen.ScreenName = theaterScreenDto.ScreenName;
                theaterScreen.ChangedBy = 1;
                theaterScreen.ChangedOn = DateTime.Now;
                await _theaterScreenService.UpdateTheaterScreen(theaterScreen);
            }
        }

        public async Task DeleteTheaterScreen(int id)
        {
            await _theaterScreenService.DeleteTheaterScreen(id);
        }

    }
}
