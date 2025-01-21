using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.Models;
using Microsoft.Identity.Client;

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
                throw new Exception("Theater does not exist for the provided id");
            }
            return TheaterDto.MapToDto(result);
        }

        public async Task AddTheater(TheaterDto theaterDto)
        {
            var exceptions = new List<string>();
            var inputTheaterName = theaterDto.TheaterName.Trim();

            if(inputTheaterName.Length <= 8) { exceptions.Add("Theater name should be more than or equals to 8 characters"); }

            var isExist = await _theaterService.GetTheaterByName(inputTheaterName);
            if(isExist != null) { exceptions.Add("Theater name already exist"); }

            if (exceptions.Count > 0) throw new CustomException(exceptions);

            var theater = new Theater
            {
                TheaterName = inputTheaterName,
                IsMultiScreen = theaterDto.IsMultiScreen,
                CreatedBy = 1
            };

            await _theaterService.AddTheater(theater);
        }

        public async Task UpdateTheater(int id, TheaterDto theaterDto)
        {
            var exceptions = new List<string>();
            var inputTheaterName = theaterDto.TheaterName.Trim();

            if (inputTheaterName.Length <= 8) { exceptions.Add("Theater name should be more than or equals to 8 characters"); }

            var isExist = await _theaterService.GetTheaterByName(inputTheaterName);
            if (isExist != null) { exceptions.Add("Theater name already exist"); }

            var theater = await _theaterService.GetTheaterById(id);
            if (theater == null) { exceptions.Add("Theater does not exist for the provided id"); }

            if (exceptions.Count > 0) throw new CustomException(exceptions);

            theater.TheaterName = theaterDto.TheaterName;
            theater.IsMultiScreen = theaterDto.IsMultiScreen;
            theater.ChangedBy = 1;
            theater.ChangedOn = DateTime.Now;

            await _theaterService.UpdateTheater();
        }

        public async Task DeleteTheater(int id)
        {
            var theater = await _theaterService.GetTheaterById(id);
            if (theater == null) throw new Exception("Theater does not exist for the provided id");
            await _theaterService.DeleteTheater(id);
        }

    }
}
