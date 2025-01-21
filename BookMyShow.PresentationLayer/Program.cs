using BookMyShow.BuinessLogicLayer.Managers;
using BookMyShow.DataAccessLayer.Abstract;
using BookMyShow.DataAccessLayer.DataContext;
using BookMyShow.DataAccessLayer.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookMyShowContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<UserManager>();

builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<MovieManager>();

builder.Services.AddTransient<IDirectorService, DirectorService>();
builder.Services.AddTransient<DirectorManager>();

builder.Services.AddTransient<IUserContactService, UserContactService>();
builder.Services.AddTransient<UserContactManager>();

builder.Services.AddTransient<IUserDetailService, UserDetailService>();
builder.Services.AddTransient<UserDetailManager>();

builder.Services.AddTransient<IActorService, ActorService>();
builder.Services.AddTransient<ActorManager>();

builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<BookingManager>();

builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<GenreManager>();

builder.Services.AddTransient<ISlotService, SlotService>();
builder.Services.AddTransient<SlotManager>();

builder.Services.AddTransient<ITheaterService, TheaterService>();
builder.Services.AddTransient<TheaterManager>();

builder.Services.AddTransient<ITheaterScreenService, TheaterScreenService>();
builder.Services.AddTransient<TheaterScreenManager>();

builder.Services.AddDbContext<BookMyShowContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
