using AutoMapper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class FilmRepo : IFilmRepo
    {
        private readonly CinemasContext _context;
        private readonly Mapper _mapper;

        public FilmRepo(CinemasContext context)
        {
            _context = context;
        }
        public int CountFilm()
        {
            return _context.Film.Count();
        }

        public bool CreateFilm(Film film)
        {
            
            _context.Film.Add(film);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteFilm(int id)
        {
            _context.Film.Remove(GetFilmById(id));
            _context.SaveChanges();
            return true;
        }

        public List<Film> GetAllFilms()
        {
            return _context.Film.ToList();
        }

        public Film GetFilmById(int id)
        {
            return _context.Film.Where(f => f.Id == id).FirstOrDefault();
        }

        public bool UpdateFilm(Film film)
        {
            _context.Entry(film).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
    }
}
