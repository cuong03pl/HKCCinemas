using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public int Count()
        {
            return _context.Category.Count();
        }

        public bool CreateCategory(CategoryViewDTO category)
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

        public List<CategoryViewDTO> GetAllCategories()
        {
            return _context.Category.Select(c => new CategoryViewDTO
            {
                Id = c.Id,
                Name = c.Name,
                Count = _context.Category.Count()
            }).ToList();
        }

        public bool UpdateCategory(int id, CategoryViewDTO category)
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

        public List<CategoryViewDTO> SearchCategory(QueryObject query)
        {
            var categories = _context.Category.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                categories = categories.Where(c => c.Name.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return categories.Select(c => new CategoryViewDTO
            {
                Id = c.Id,
                Name = c.Name,
                Count = categories.Count()
            }).Skip(skipNumber).Take(query.PageSize).ToList();


        }

        
    }
}
