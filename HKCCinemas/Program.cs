using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using HKCCinemas.Repo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IActorRepo, ActorRepo>();
builder.Services.AddScoped<IFilmRepo, FilmRepo>();
builder.Services.AddScoped<ICinemasRepo, CinemasRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddDbContext<CinemasContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("HKCCinemasContext"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
var app = builder.Build();
app.UseCors("AllowOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
