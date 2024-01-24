using AutoMapper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly CinemasContext _context;
        private readonly Mapper _mapper;

        public UserRepo(CinemasContext context) {
            _context = context;
        }

        public int GetCountUser()
        {
            return _context.User.Count();
        }

        public bool CreateUser(User user)
        {
           _context.User.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            _context.User.Remove(GetUserById(id));
            _context.SaveChanges(); 
            return true;
        }

        public List<User> GetAllUsers()
        {
            return _context.User.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.User.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUserByUserName(string username)
        {
            return _context.User.Where(u => u.Username == username).FirstOrDefault();

        }

        public bool UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
    }
}
