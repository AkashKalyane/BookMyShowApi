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
            var filteredData = directors.Where(x => x.DeletedBy == null);
            return filteredData.Select(x => DirectorDto.MapToDto(x)).ToList();
        }

        public async Task<DirectorDto> GetDirectorById(int id)
        {
            var result = await _directorService.GetDirectorById(id);
            if (result == null)
            {
                return null;
            }
            return DirectorDto.MapToDto(result);
        }

        public async Task AddDirector(DirectorDto directorDto)
        {
            if (directorDto != null)
            {
                var director = new Director
                {
                    Name = directorDto.Name,
                    Age = directorDto.Age,
                    TypeOfMovies = directorDto.TypeOfMovies,
                    CreatedBy = 1
                };
                await _directorService.AddDirector(director);
            }
        }

        public async Task UpdateDirector(int id, DirectorDto directorDto)
        {
            if (directorDto != null)
            {
                var director = await _directorService.GetDirectorById(id);
                director.Name = directorDto.Name;
                director.Age = directorDto.Age;
                director.TypeOfMovies = directorDto.TypeOfMovies;
                director.ChangedBy = 1;
                director.ChangedOn = DateTime.Now;
                await _directorService.UpdateDirector(director);
            }
        }

        public async Task DeleteDirector(int id)
        {
            await _directorService.DeleteDirector(id);
        }

    }
}
