using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace HKCCinemas.Repo
{
    public class FilmRepo : IFilmRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        public FilmRepo(CinemasContext context, IMapper mapper, IWebHostEnvironment evn)
        {
            _context = context;
            _mapper = mapper;  
            _evn = evn;
        }
        public int CountFilm()
        {
            return _context.Film.Count();
        }

        public async Task<bool> CreateFilmAsync(FilmDTO film)
        {
            
            
            if (film.formFile != null)
            {
                var fileName = film.formFile.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await film.formFile.CopyToAsync(stream);
                }

                film.Thumbnail = pathToSave;
            }

            var filmMapper = _mapper.Map<Film>(film);
            foreach (var item in film.categoryIds)
            {
                var cate = _context.Category.Where(c => c.Id == item).FirstOrDefault();

                var categoryFilm = new CategoryFilm()
                {
                    Film = filmMapper,
                    Category = cate
                };
                _context.CategoryFilm.Add(categoryFilm);
            }
            
            
            _context.Film.Add(filmMapper);
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

        public async Task<bool> UpdateFilmAsync(FilmDTO film)
        {
            if (film.formFile != null)
            {
                var fileName = film.formFile.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await film.formFile.CopyToAsync(stream);
                }

                film.Thumbnail = pathToSave;
            }

            var filmMapper = _mapper.Map<Film>(film);
            foreach (var item in film.categoryIds)
            {
                var cate = _context.Category.Where(c => c.Id == item).FirstOrDefault();

                var categoryFilm = new CategoryFilm()
                {
                    Film = filmMapper,
                    Category = cate
                };
                _context.CategoryFilm.Update(categoryFilm);
            }
            _context.Film.Update(filmMapper);
            _context.SaveChanges();

            return true;
            
        }
    }
}
