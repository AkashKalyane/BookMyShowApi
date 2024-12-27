using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShow.DataAccessLayer.Models;

namespace BookMyShow.BuinessLogicLayer.DTOs
{
    public class GenreDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public static GenreDto MapToDto(Genre genre) => new GenreDto()
        {
            GenreId = genre.GenreId,
            GenreName = genre.GenreName,
        };
    }
}
