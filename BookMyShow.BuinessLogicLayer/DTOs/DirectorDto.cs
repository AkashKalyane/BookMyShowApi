using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class DirectorDto
    {
        public int DirectorId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string TypeOfMovies { get; set; }
        public static DirectorDto MapToDto(Director director) => new DirectorDto
        {
            DirectorId = director.DirectorId,
            Name = director.Name,
            Age = director.Age,
            TypeOfMovies = director.TypeOfMovies
        };
    }
}

