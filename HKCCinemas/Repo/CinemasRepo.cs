using AutoMapper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class CinemasRepo : ICinemasRepo
    {
        private readonly CinemasContext _context;
        private readonly Mapper _mapper;

        public CinemasRepo(CinemasContext context)
        {
            _context = context;
        }
        public int CountCinemas()
        {
            return _context.Cinemas.Count();
        }

        public bool CreateCinemas(Cinemas cinemas)
        {
            _context.Cinemas.Add(cinemas);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCinemas(int id)
        {
            _context.Cinemas.Remove(GetCinemasById(id));
            _context.SaveChanges();
            return true;
        }

        public List<Cinemas> GetAllCinemas()
        {
            return _context.Cinemas.ToList();
        }

        public Cinemas GetCinemasById(int id)
        {
          return  _context.Cinemas.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool UpdateCinemas(Cinemas cinemas)
        {
            _context.Entry(cinemas).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
    }
}
