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
    public class DirectorManager
    {
        private readonly IDirectorService _directorService;

        public DirectorManager(IDirectorService directorService)
        {
            this._directorService = directorService;
        }

        public async Task<List<DirectorDto>> GetDirectors()
        {
            var directors = await _directorService.GetDirectors();
            return directors.Select(x => DirectorDto.MapToDto(x)).ToList();
        }

        public async Task<DirectorDto> GetDirectorById(int id)
        {
            var result = await _directorService.GetDirectorById(id);
            if (result == null)
            {
                throw new Exception("Director does not exist for the provided id");
            }
            return DirectorDto.MapToDto(result);
        }

        public async Task AddDirector(DirectorDto directorDto)
        {
            var exceptions = new List<string>();

            var inputName = directorDto.Name.Trim();
            var inputTypeOfMovies = directorDto.TypeOfMovies.Trim();
            if (inputName.Length < 6) { exceptions.Add("Director name should be more than 6 characters and it should be first and last name"); }
            if(inputTypeOfMovies.Length < 3) { exceptions.Add("Type of movie should be more than 3 characters"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            var director = new Director
            {
                Name = inputName,
                Age = directorDto.Age,
                TypeOfMovies = inputTypeOfMovies,
                CreatedBy = 1
            };

            await _directorService.AddDirector(director);
        }

        public async Task UpdateDirector(int id, DirectorDto directorDto)
        {
            var exceptions = new List<string>();

            var inputName = directorDto.Name.Trim();
            var inputTypeOfMovies = directorDto.TypeOfMovies.Trim();
            if (inputName.Length < 6) { exceptions.Add("Director name should be more than 6 characters and it should be first and last name"); }
            if (inputTypeOfMovies.Length < 3) { exceptions.Add("Type of movie should be more than 3 characters"); }

            var director = await _directorService.GetDirectorById(id);
            if(director == null) {  exceptions.Add("Director does not exist for the provided id"); }

            if (exceptions.Count > 0) { throw new CustomException(exceptions); }

            director.Name = inputName;
            director.Age = directorDto.Age;
            director.TypeOfMovies = inputTypeOfMovies;
            director.ChangedBy = 1;
            director.ChangedOn = DateTime.Now;

            await _directorService.UpdateDirector();
        }

        public async Task DeleteDirector(int id)
        {
            var director = await _directorService.GetDirectorById(id);
            if (director == null) { throw new Exception("Director does not exist for the provided id"); }

            await _directorService.DeleteDirector(id);
        }

    }
}
