using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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
        public int Count()
        {
            return _context.CinemasCategories.Count();
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

        public List<CinemasCategoryViewDTO> GetCinemasCategories()
        {
            return _context.CinemasCategories.Select(cc => new CinemasCategoryViewDTO
            {
                Id = cc.Id,
                Name = cc.Name,
                Count = _context.CinemasCategories.Count(),
                Image = cc.Image,
            }).ToList();
        }

        public CinemasCategory GetCinemasCategory(int id)
        {
            return _context.CinemasCategories.Where(cc => cc.Id == id).FirstOrDefault();
        }

        public List<CinemasCategoryViewDTO> Search(QueryObject query)
        {
            var cinemasCategories = _context.CinemasCategories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                cinemasCategories = cinemasCategories.Where(a => a.Name.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return cinemasCategories.Select(cc => new CinemasCategoryViewDTO
            {
                Id = cc.Id,
                Name = cc.Name,
                Count = cinemasCategories.Count(),
                Image = cc.Image,
            }).Skip(skipNumber).Take(query.PageSize).ToList();
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
