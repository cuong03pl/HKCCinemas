using AutoMapper;
using HKCCinemas.DTO;
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
        public int CountCinemas()
        {
            return _context.Cinemas.Count();
        }

        public async Task<bool> CreateCinemasAsync(CinemasDTO cinemas)
        {
            if (cinemas.formFile != null)
            {
                var fileName = cinemas.formFile.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await cinemas.formFile.CopyToAsync(stream);
                }

                cinemas.Image = pathToSave;
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

        public List<Cinemas> GetAllCinemas()
        {
            return _context.Cinemas.ToList();
        }

        public Cinemas GetCinemasById(int id)
        {
          return  _context.Cinemas.Where(c => c.Id == id).FirstOrDefault();
        }

        public async Task<bool> UpdateCinemas(int id, CinemasDTO cinemas)
        {
            var cinemasNow = _context.Cinemas.Where(c => c.Id == id).FirstOrDefault();
            if (cinemas.formFile != null && cinemas.formFile.Length > 0)
            {
                var fileName = cinemas.formFile.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await cinemas.formFile.CopyToAsync(stream);
                }

                cinemas.Image = pathToSave;
            }
            else
            {
                cinemas.Image = cinemasNow.Image;
            }
            cinemasNow.Name = cinemas.Name;
            cinemasNow.Address = cinemas.Address;
            cinemasNow.Image = cinemas.Image;
            _context.Cinemas.Update(cinemasNow);
            _context.SaveChanges();
            return true;
        }
    }
}
