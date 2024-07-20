using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;

namespace HKCCinemas.Repo
{
    public class CinemasCategoryRepo : ICinemasCategoryRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;

        public CinemasCategoryRepo(CinemasContext context, IMapper mapper, IWebHostEnvironment evn)
        {
            _context = context;
            _mapper = mapper;
            _evn = evn;
        }

        public async Task<bool> CreateCinemasCategory(CinemasCategoryDTO category)
        {
            if(category.formFile != null) {
                var fileName = category.formFile.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await category.formFile.CopyToAsync(stream);
                }

                category.Image = pathToSave;
            }

            var categoryMapper = _mapper.Map<CinemasCategory>(category);
            _context.CinemasCategories.Add(categoryMapper);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCinemasCategory(int id)
        {
            var category = GetCinemasCategory(id);
            _context.CinemasCategories.Remove(category);
            _context.SaveChanges();
            return true;
        }

        public List<CinemasCategory> GetCinemasCategories()
        {
            return _context.CinemasCategories.ToList();
        }

        public CinemasCategory GetCinemasCategory(int id)
        {
            return _context.CinemasCategories.Where(cc => cc.Id == id).FirstOrDefault();
        }

        public async Task<bool> UpdateCategoryCinemas(int id, CinemasCategoryDTO category)
        {
            var categoryNow = _context.CinemasCategories.Where(cc => cc.Id == id).FirstOrDefault();
            if (category.formFile != null && category.formFile.Length > 0)
            {
                var fileName = category.formFile.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await category.formFile.CopyToAsync(stream);
                }

                category.Image = pathToSave;
            }
            else
            {
                category.Image = categoryNow.Image;
            }
            categoryNow.Name = category.Name;
            _context.CinemasCategories.Update(categoryNow);
            _context.SaveChanges();
            return true;
        }
    }
}
