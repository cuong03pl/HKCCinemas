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
        private readonly IMapper _mapper;

        public CategoryRepo(CinemasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int CountCategory()
        {
            return _context.Actor.Count();
        }

        public bool CreateCategory(CategoryDTO category)
        {
            var categoryMapper = _mapper.Map<Category>(category);
            _context.Category.Add(categoryMapper);
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

        public List<Category> GetAllCategoriesByFilmId(int film_id)
        {

            var categories =  _context.CategoryFilm
                .Where(cf => cf.FilmId == film_id)
                .Select(cf => cf.Category)
                .ToList();

            return categories;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Category.ToList();
        }

        public bool UpdateCategory(int id, CategoryDTO category)
        {
            var categoryNow = _context.Category.Where(f => f.Id == id).FirstOrDefault();
            categoryNow.Name = category.Name;
            _context.Category.Update(categoryNow);
            _context.SaveChanges();
            return true;
        }

        public List<int> GetAllCategoryIdsByFilmId(int film_id)
        {
            var categories = _context.CategoryFilm
               .Where(cf => cf.FilmId == film_id)
               .Select(cf => cf.Category.Id)
               .ToList();

            return categories;
        }

        public List<Category> SearchCategory(string keyword)
        {
            return _context.Category.Where(c => c.Name.Contains(keyword)).ToList();
        }
    }
}
