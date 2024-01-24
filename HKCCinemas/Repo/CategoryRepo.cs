using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly CinemasContext _context;
        private readonly Mapper _mapper;

        public CategoryRepo(CinemasContext context)
        {
            _context = context;
        }
        public int CountCategory()
        {
            return _context.Actor.Count();
        }

        public bool CreateCategory( int filmId,Category category)
        {
            var film = _context.Film.Where(film => film.Id == filmId).FirstOrDefault();

            var categoryFilm = new CategoryFilm()
            {
                Film = film,
                Category = category
            };
            _context.CategoryFilm.Add(categoryFilm);
            _context.Category.Add(category);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCategory(int id)
        {
            _context.Category.Remove(GetCategoryById(id));
            _context.SaveChanges(); 
            return true;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Category.Where(a => a.Id == id).FirstOrDefault();
        }

        public List<Category> GetAllCategories(int film_id)
        {
            var film = _context.Film.Where(f => f.Id == film_id).Include(f => f.categoryFilms).ThenInclude(f => f.Category).FirstOrDefault();
            var actorDtos = film.categoryFilms.Select(f => f.Category).ToList();
            return actorDtos;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
    }
}
