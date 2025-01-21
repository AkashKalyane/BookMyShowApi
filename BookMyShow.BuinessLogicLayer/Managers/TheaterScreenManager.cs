using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.CustomExceptions;
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
                throw new Exception("Theater screen does not exist for the provided id");
            }
            return TheaterScreenDto.MapToDto(result);
        }

        public async Task AddTheaterScreen(TheaterScreenDto theaterScreenDto)
        {
            var exceptions = new List<string>();

            var inputScreenName = theaterScreenDto.ScreenName.Trim();
            var inputTheaterId = theaterScreenDto.TheaterId;
            if (inputScreenName.Length < 3) { exceptions.Add("Screen name should be more than 3 characters"); }
            if(inputTheaterId == null) { exceptions.Add("Please provide theater id"); }

            var theater = await _theaterScreenService.verifydata(theaterScreenDto.TheaterId, inputScreenName);

            if (theater != null) { exceptions.AddRange(theater); }

            if(exceptions.Count > 0) { throw new CustomException(exceptions); }

            var theaterScreen = new TheaterScreen
            {
                TheaterId = inputTheaterId,
                ScreenName = inputScreenName,
                CreatedBy = 1
            };

            await _theaterScreenService.AddTheaterScreen(theaterScreen);
        }

        public async Task UpdateTheaterScreen(int id, TheaterScreenDto theaterScreenDto)
        {
            var exceptions = new List<string>();

            var inputTheaterId = theaterScreenDto.TheaterId;
            var inputScreenName = theaterScreenDto.ScreenName.Trim();
            if (inputScreenName.Length < 3) { exceptions.Add("Screen name should be more than 3 characters"); }
            if (inputTheaterId == null) { exceptions.Add("Please provide theater id"); }
            
            var theaterScreen = await _theaterScreenService.GetTheaterScreenById(id);
            if(theaterScreen == null) { exceptions.Add("Theater screen does not exist for the provided id"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            theaterScreen.TheaterId = inputTheaterId;
            theaterScreen.ScreenName = inputScreenName;
            theaterScreen.ChangedBy = 1;
            theaterScreen.ChangedOn = DateTime.Now;

            await _theaterScreenService.UpdateTheaterScreen();
        }

        public async Task DeleteTheaterScreen(int id)
        {
            var theaterScreen = await _theaterScreenService.GetTheaterScreenById(id);
            if (theaterScreen == null) { throw new Exception("Theater screen does not exist for the provided id"); }

            await _theaterScreenService.DeleteTheaterScreen(id);
        }

    }
}
