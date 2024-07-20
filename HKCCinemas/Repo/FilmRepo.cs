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
        
        // get
        public int CountFilm()
        {
            return _context.Film.Count();
        }
        public List<Film> GetAllFilms()
        {
            return _context.Film.ToList();
        }
        public List<Film> GetTop5Films()
        {
            return _context.Film.OrderByDescending(f => f.Rating).Take(5).ToList();
        }
        public Film GetFilmById(int id)
        {
            return _context.Film.Where(f => f.Id == id).FirstOrDefault();
        }
        public List<Film> GetNowShowingFilms()
        {
            DateTime now = DateTime.Now;
            return _context.Film.Where(f => f.ReleaseDate <= now && f.EndDate >= now).ToList();
        }
        public List<Film> GetUpcomingFilms()
        {
            DateTime now = DateTime.Now;
            return _context.Film.Where(f =>  f.ReleaseDate > now).ToList();
        }
        public List<Film> GetSimilarFilm(int filmId)
        {
            var categoryIs = _context.CategoryFilm.Where(cf => cf.FilmId == filmId).Select(cf => cf.CategoryId).ToList();
            var data = _context.CategoryFilm.Where(cf => categoryIs.Contains(cf.CategoryId)).Include(cf => cf.Film).Select(cf => cf.Film).Distinct().ToList();  
            return data;
        }
        // create
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

                film.Image = pathToSave;
            }
            if (film.formFileBackground != null)
            {
                var fileName = film.formFileBackground.FileName;
                var webPath = _evn.WebRootPath;
                var path = Path.Combine("", webPath + @"\Images\" + fileName);
                var pathToSave = @"/Images/" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await film.formFileBackground.CopyToAsync(stream);
                }

                film.Background = pathToSave;
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

        //delete
        public bool DeleteFilm(int id)
        {
            _context.Film.Remove(GetFilmById(id));
            _context.SaveChanges();
            return true;
        }

        //update
        public async Task<bool> UpdateFilmAsync(int id, FilmDTO film)
        {
            try
            {
                var filmNow = _context.Film.Where(f => f.Id == id).FirstOrDefault();

                if (film.formFile != null && film.formFile.Length > 0)
                {
                    var fileName = film.formFile.FileName;
                    var webPath = _evn.WebRootPath;
                    var path = Path.Combine("", webPath + @"\Images\" + fileName);
                    var pathToSave = @"/Images/" + fileName;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await film.formFile.CopyToAsync(stream);
                    }
                    film.Image = pathToSave;
                }
                else
                {
                    film.Image = filmNow.Image;
                }

                if (film.formFileBackground != null && film.formFileBackground.Length > 0)
                {
                    var fileName2 = film.formFileBackground.FileName;
                    var webPath2 = _evn.WebRootPath;
                    var path2 = Path.Combine("", webPath2 + @"\Images\" + fileName2);
                    var pathToSave2 = @"/Images/" + fileName2;
                    using (var stream = new FileStream(path2, FileMode.Create))
                    {
                        await film.formFileBackground.CopyToAsync(stream);
                    }

                    film.Background = pathToSave2;
                }
                else
                {
                    film.Background = filmNow.Background;
                }
                filmNow.Title = film.Title;
                filmNow.Detail = film.Detail;
                filmNow.Synopsis = film.Synopsis;
                filmNow.AgeLimit = film.AgeLimit;
                filmNow.Duration = film.Duration;
                filmNow.Country = film.Country;
                filmNow.Rating = film.Rating;
                filmNow.ReleaseDate = film.ReleaseDate;
                filmNow.EndDate = film.EndDate;
                filmNow.Director = film.Director;
                filmNow.Image = film.Image;
                filmNow.Background = film.Background;
                // xóa các categoryFilm cũ
                if (film.categoryIds != null)
                {
                    var filmCategoryOld = _context.CategoryFilm.Where(c => c.FilmId == id);
                    foreach (var c in filmCategoryOld)
                    {
                        _context.CategoryFilm.Remove(c);
                    }

                    foreach (var item in film.categoryIds)
                    {
                        var cate = _context.Category.Where(c => c.Id == item).FirstOrDefault();
                        var categoryFilm = new CategoryFilm()
                        {
                            Film = filmNow,
                            Category = cate
                        };
                        _context.CategoryFilm.Add(categoryFilm);
                    }
                }

                _context.Film.Update(filmNow);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Film> GetAllFilmsByQuery(string query)
        {
            var data = _context.Film.Where(f => f.Title.Contains(query)).ToList();
            return data;
        }

        public List<Film> GetAllFilmByCategory(int cateId)
        {
            var data = _context.CategoryFilm.Where(c => c.CategoryId == cateId).Select(c => c.Film).ToList();
            return data;
        }

        public List<int> GetAllFilmByActor(int actorId)
        {
            var data = _context.FilmActors.Where(c => c.actorId == actorId).Select(c => c.Film.Id).ToList();
            return data;
        }

    }
}
