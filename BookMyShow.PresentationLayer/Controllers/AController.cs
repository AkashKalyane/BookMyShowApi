using System.Dynamic;
using System.IO.Pipes;
using System.Text.RegularExpressions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.DataAccessLayer.DataContext;
using BookMyShow.DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AController : ControllerBase
    {
        private readonly BookMyShowContext _context;

        public AController(BookMyShowContext context) { this._context = context; }

        [HttpGet]
        public ActionResult test()
        {
            var users = _context.Users.ToList();

            var result = from user in _context.Users
                         where user.UserId == 10
                         select user;

            var result2 = users.All(x => x.CreatedBy.StartsWith("A") || x.CreatedBy.StartsWith("a"));

            var result3 = users.Select(x => x.CreatedBy);

            var result4 = users.Where(x => x.Salt != null);

            var result5 = from user in _context.Users
                          select new
                          {
                              user.UserId,
                              user.UserName,
                              user.Email,
                              user.Password
                          };

            var inputPhoneNumber = "4343441324";
            var result6 = from contact in _context.UserContacts
                          where contact.PhoneNumber == inputPhoneNumber
                          select new UserContactDto()
                          {
                              UserContactId = contact.UserContactId,
                              PhoneNumber = contact.PhoneNumber,
                              UserId = contact.UserId,
                          };

            var result7 = _context.Users.Where(x => x.UserId == 100).FirstOrDefault();

            var result8 = result5.OrderBy(x => x.UserName);

            var movies = from movie in _context.Movies
                         select new
                         {
                             movie.MovieId,
                             movie.MovieName,
                             movie.Grade,
                             movie.ReleaseYear
                         };

            var result9 = from movie in movies
                          group movie by movie.Grade into grade
                          orderby grade.Key
                          select new { grade.Key, grade };

            var result10 = _context.Movies.
                Where(x => x.MovieName == "The Dark Knight").
                Select(x => x.MovieId).FirstOrDefault();


            var result11 = _context.Bookings.
                Where(x => x.MovieId == result10).
                Select(x => new
                {
                    x.BookingId,
                    x.MovieId,
                    x.NumberOfSlot,
                    x.Price,
                    x.TheaterScreen
                });

            var result12 = _context.Movies.
                GroupBy(x => x.GenreId).
                Select(x => new { key = x.Key, count = x.Count() });

            var result13 = _context.Slots.
                Where(s => _context.Bookings.Count(b => s.SlotId == b.SlotId) > 0).
                Select(a => new
                {
                    a.SlotId,
                    a.SlotName,
                    count = _context.Bookings.Count(b => b.SlotId == a.SlotId)
                });

            var result14 = _context.Genres.
                Where(g => !_context.Movies.Select(m => m.GenreId).Contains(g.GenreId)).
                Select(x => new
                {
                    x.GenreId,
                    x.GenreName
                });

            var result15 = _context.Directors.
                Select(x => new
                {
                    x.DirectorId,
                    x.Name,
                    count = _context.Movies.Count(m => m.DirectorId == x.DirectorId)
                }).
                Where(x => x.count > 0);

            //var result15 = _context.Movies.
            //    GroupBy(x => x.DirectorId).
            //    Select(x => new
            //    {
            //        x.Key,
            //        total = x.Count()
            //    });

            var result16 = _context.Bookings.
                Sum(x => x.Price * x.NumberOfSlot);

            var result17 = _context.Movies.
                Where(x => x.GenreId == _context.Genres.
                    Where(g => g.GenreName == "Action").
                    Select(g => g.GenreId).FirstOrDefault() &&
                            x.DirectorId == _context.Directors.
                    Where(d => d.Name == "Steven Spielberg").
                    Select(d => d.DirectorId).FirstOrDefault()).
                Select(y => new { y.MovieId, y.MovieName, y.DirectorId, y.GenreId });

            var result18 = from user in _context.Users
                           join detail
                           in _context.UserDetails on user.UserId equals detail.UserId
                           select new
                           {
                               user.UserId,
                               detail.FirstName,
                               detail.LastName
                           };

            var result19 = _context.Users.Join(_context.UserDetails, u => u.UserId, ud => ud.UserId,
                (u, ud) => new
                {
                    u.UserId,
                    ud.FirstName,
                    ud.LastName
                });

            var result20 = _context.Users.Join(_context.UserDetails, u => u.UserId, ud => ud.UserId,
                (u, ud) => new { u, ud }).Join(_context.UserContacts, x => x.ud.UserId, uc => uc.UserId,
                (x, uc) => new
                {
                    x.u.UserId,
                    x.u.UserName,
                    x.ud.FirstName,
                    x.ud.LastName,
                    uc.PhoneNumber,
                    uc.CountryCode
                });

            var result21 = _context.Bookings.Join(_context.Movies, b => b.MovieId, m => m.MovieId,
                (b, m) => new
                {
                    b.MovieId,
                    m.MovieName,
                    b.Price
                });

            var result22 = _context.Users.
                Select(x => new
                {
                    x.UserId,
                    x.UserName,
                    x.Email,
                    x.Password,
                    x.Salt,
                });

            var result23 = _context.Bookings.
                Include(b => b.Movie).
                Include("Movie.Director").
                //ThenInclude(d => d.Director).
                Include(s => s.Slot).ToList();

            var result24 = _context.Bookings.
                Include(m => m.Movie).
                ThenInclude(g => g.Genre).
                Include(s => s.Slot).
                Include(t => t.TheaterScreen).Select(x => new
                {
                    x.BookingId,
                    x.MovieId,
                    x.Movie.MovieName,
                    x.Movie.MainActorFemale,
                    x.Movie.MainActorMale,
                    x.Price,
                    x.SlotId,
                    x.Slot.SlotName,
                    x.TheaterScreenId,
                    x.TheaterScreen.ScreenName,
                    x.NumberOfSlot,
                }).ToList();

            var result25 = _context.Bookings.AsNoTracking().
                Select(x => new Booking()
                {
                    BookingId = x.BookingId,
                    MovieId = x.MovieId,
                    Movie = new Movie()
                    {
                        MovieId = x.Movie.MovieId,
                        MovieName = x.Movie.MovieName,
                        MainActorMaleNavigation = x.Movie.MainActorMaleNavigation == null ? new Actor() : new Actor()
                        {
                            Name = x.Movie.MainActorMaleNavigation.Name
                        },
                        MainActorMale = x.Movie.MainActorMale,
                        GenreId = x.Movie.GenreId,
                        Genre = new Genre()
                        {
                            GenreId = x.Movie.Genre.GenreId,
                            GenreName = x.Movie.Genre.GenreName,
                        },
                    },
                    TheaterScreenId = x.TheaterScreenId,
                    TheaterScreen = new TheaterScreen()
                    {
                        TheaterScreenId = x.TheaterScreen.TheaterScreenId,
                        ScreenName = x.TheaterScreen.ScreenName
                    }
                });


            return Ok(result25);

            var t = _context.Bookings.Select(x => new Booking()
            {
                BookingId = x.BookingId,
                Movie = new Movie()
                {

                }
            }).AsNoTracking();

        }
    }
}

