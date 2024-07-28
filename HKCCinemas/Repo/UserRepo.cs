using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HKCCinemas.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<User> userManager;
        private readonly Mapper _mapper;

        public UserRepo(UserManager<User> _userManager) {
            userManager = _userManager;
        }

        public List<UserViewDTO> GetAllUsers()
        {
            return userManager.Users.Select(u => new UserViewDTO
            {
                AccessFailedCount = u.AccessFailedCount,
                Avatar = u.Avatar,
                ConcurrencyStamp = u.ConcurrencyStamp,
                Count = userManager.Users.Count(),
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                Id = u.Id,
                LockoutEnabled = u.LockoutEnabled,
                LockoutEnd= u.LockoutEnd,
                NormalizedEmail = u.NormalizedEmail,
                NormalizedUserName = u.NormalizedUserName,
                PasswordHash = u.PasswordHash,
                PhoneNumber = u.PhoneNumber,
                PhoneNumberConfirmed   = u.PhoneNumberConfirmed,
                SecurityStamp = u.SecurityStamp,
                TwoFactorEnabled = u.TwoFactorEnabled,
                UserName = u.UserName,
               
            }).ToList();
        }
        public User GetUserById(string id)
        {
            return userManager.Users.Where(u => u.Id == id).FirstOrDefault();
        }
        public User GetUserByUserName(string username)
        {
            return userManager.Users.Where(u => u.UserName == username).FirstOrDefault();
        }
        public async Task<bool> DeleteUser(string id)
        {
            var user = GetUserById(id);
            if(user == null)
            {
                return false;
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
        public int Count()
        {
            return userManager.Users.Count();
        }

        public List<UserViewDTO> Search(QueryObject query)
        {
            var users = userManager.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                users = users.Where(c => c.UserName.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return users.Select(u => new UserViewDTO
            {
                AccessFailedCount = u.AccessFailedCount,
                Avatar = u.Avatar,
                ConcurrencyStamp = u.ConcurrencyStamp,
                Count = userManager.Users.Count(),
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                Id = u.Id,
                LockoutEnabled = u.LockoutEnabled,
                LockoutEnd = u.LockoutEnd,
                NormalizedEmail = u.NormalizedEmail,
                NormalizedUserName = u.NormalizedUserName,
                PasswordHash = u.PasswordHash,
                PhoneNumber = u.PhoneNumber,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                SecurityStamp = u.SecurityStamp,
                TwoFactorEnabled = u.TwoFactorEnabled,
                UserName = u.UserName,
            }).Skip(skipNumber).Take(query.PageSize).ToList();
        }
    }
}
