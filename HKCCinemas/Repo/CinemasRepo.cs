using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class CinemasRepo : ICinemasRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        public CinemasRepo(CinemasContext context, IMapper mapper, IWebHostEnvironment evn)
        {
            _context = context;
            _mapper = mapper;
            _evn = evn;
        }
        public int Count()
        {
            return _context.Cinemas.Count();
        }

        public async Task<bool> CreateCinemasAsync(CinemasDTO cinemas)
        {
            if (cinemas.formFileImage != null)
            {
                var fileName = cinemas.formFileImage.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await cinemas.formFileImage.CopyToAsync(stream);
                }

                cinemas.Image = pathToSave;
            }
            if (cinemas.formFileBackground != null)
            {
                var fileName = cinemas.formFileBackground.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await cinemas.formFileBackground.CopyToAsync(stream);
                }

                cinemas.Background = pathToSave;
            }

            var cinemasMapper = _mapper.Map<Cinemas>(cinemas);
            _context.Cinemas.Add(cinemasMapper);
            _context.SaveChanges();

            return true;

        }

        public bool DeleteCinemas(int id)
        {
            _context.Cinemas.Remove(GetCinemasById(id));
            _context.SaveChanges();
            return true;
        }

        public List<CinemasViewDTO> GetAllCinemas()
        {
            
            return _context.Cinemas.Select(c => new CinemasViewDTO
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Image = c.Image,
                Background = c.Background,
                CinemasCategoryId = c.CinemasCategoryId,
                Count = _context.Cinemas.Count()
            }).ToList();
        }
        public List<Cinemas> GetCinemasByCategoryId(int categoryId)
        {
            return _context.Cinemas.Where(c => c.CinemasCategoryId == categoryId).ToList();
        }
        public Cinemas GetCinemasById(int id)
        {
            return _context.Cinemas.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<CinemasViewDTO> SearchCinemas(QueryObject query)
        {

            var cinemas = _context.Cinemas.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                cinemas = cinemas.Where(c => c.Name.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return cinemas.Select(c => new CinemasViewDTO
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Image = c.Image,
                Background = c.Background,
                CinemasCategoryId = c.CinemasCategoryId,
                Count = cinemas.Count(),
            }).Skip(skipNumber).Take(query.PageSize).ToList();
        }

        public async Task<bool> UpdateCinemas(int id, CinemasDTO cinemas)
        {
            var cinemasNow = _context.Cinemas.Where(c => c.Id == id).FirstOrDefault();
            if (cinemas.formFileImage != null && cinemas.formFileImage.Length > 0)
            {
                var fileName = cinemas.formFileImage.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await cinemas.formFileImage.CopyToAsync(stream);
                }

                cinemas.Image = pathToSave;
            }
            else
            {
                cinemas.Image = cinemasNow.Image;
            }
            if (cinemas.formFileBackground != null && cinemas.formFileBackground.Length > 0)
            {
                var fileName2 = cinemas.formFileBackground.FileName;
                var webPath2 = _evn.WebRootPath;
                var path2 = Path.Combine("", webPath2 + @"\Images\" + fileName2);
                var pathToSave2 = @"/Images/" + fileName2;
                using (var stream = new FileStream(path2, FileMode.Create))
                {
                    await cinemas.formFileBackground.CopyToAsync(stream);
                }

                cinemas.Background = pathToSave2;
            }
            else
            {
                cinemas.Background = cinemasNow.Background;
            }

            cinemasNow.Name = cinemas.Name;
            cinemasNow.Address = cinemas.Address;
            cinemasNow.Image = cinemas.Image;
            cinemasNow.Background = cinemas.Background;
            cinemasNow.CinemasCategoryId = cinemasNow.CinemasCategoryId;
            _context.Cinemas.Update(cinemasNow);
            _context.SaveChanges();
            return true;
        }
    }
}
