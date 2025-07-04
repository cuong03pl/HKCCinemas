﻿using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using HKCCinemas.Repo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<ITrailerRepo, TrailerRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<ICommentRepo, CommentRepo>();
builder.Services.AddScoped<ICinemasCategoryRepo, CinemasCategoryRepo>();
builder.Services.AddScoped<IRoomRepo, RoomRepo>();
builder.Services.AddScoped<IScheduleRepo, ScheduleRepo>();
builder.Services.AddScoped<ISeatRepo, SeatRepo>();
builder.Services.AddScoped<IShowDateRepo, ShowDateRepo>();
builder.Services.AddScoped<ITicketRepo, TicketRepo>();
builder.Services.AddScoped<IBookingUserRepo, BookingUserRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IFavouriteRepo, FavouriteRepo>();
builder.Services.AddScoped<ISendMailService, SendMailService>();
builder.Services.AddScoped<RandomAvatar>();
builder.Services.AddDbContext<CinemasContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("HKCCinemasContext"));
});

var emailOptions = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(emailOptions);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<CinemasContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
    };
});

var app = builder.Build();
app.UseCors("AllowOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();