using AutoMapper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<User> userManager;
        private readonly Mapper _mapper;

        public UserRepo(UserManager<User> _userManager) {
            userManager = _userManager;
        }
        public List<User> GetAllUsers()
        {
            return userManager.Users.ToList();
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
        public int GetCountUser()
        {
            return userManager.Users.Count();
        }

        public List<User> Search(string keyword)
        {
            return userManager.Users.Where(u => u.UserName.Contains(keyword)).ToList();
        }
    }
}
