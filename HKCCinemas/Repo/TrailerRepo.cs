using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HKCCinemas.Repo
{
    public class TrailerRepo : ITrailerRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        public TrailerRepo(CinemasContext context, IMapper mapper, IWebHostEnvironment evn)
        {
            _context = context;
            _mapper = mapper;
            _evn = evn;
        }

        public int Count()
        {
            return _context.Trailers.Count();
        }
        // get
        public List<TrailerViewDTO> GetAllTrailer()
        {
            return _context.Trailers.Select(t => new TrailerViewDTO
            {
                Count = _context.Trailers.Count(),
                FilmId = t.FilmId,
                Id = t.Id,
                Link = t.Link,
            }).ToList();
        }
        public List<Trailer> GetAllTrailerByFilmId(int film_id)
        {
            return _context.Trailers.Where(t => t.FilmId == film_id).ToList();
        }
        
        // create
        public async Task<bool> CreateTrailerAsync(TrailerDTO trailer)
        {
            var trailerMapper = _mapper.Map<Trailer>(trailer);
            _context.Trailers.Add(trailerMapper);
            _context.SaveChanges();
            return true;
        }

        //delete
        public bool DeleteTrailer(int trailer_id)
        {
            var trailer = _context.Trailers.Where(t => t.Id == trailer_id).FirstOrDefault();
            if (trailer != null)
            {
                _context.Trailers.Remove(trailer);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        //update
        public async Task<bool> UpdateTrailerAsync(int id, TrailerDTO trailer)
        {
            var trailerNow = _context.Trailers.Where(t => t.Id == id).FirstOrDefault();
            trailerNow.Link = trailer.Link;
            trailerNow.FilmId = trailer.FilmId;
            _context.Trailers.Update(trailerNow);
            _context.SaveChanges();
            return true;
        }

        public List<TrailerViewDTO> Search(QueryObject query)
        {
            var trailers = _context.Trailers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                trailers = trailers.Where(c => c.Film.Title.Contains(query.Keyword));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return trailers.Select(t => new TrailerViewDTO
            {
                Count = trailers.Count(),
                FilmId = t.FilmId,
                Id = t.Id,
                Link = t.Link,
            }).Skip(skipNumber).Take(query.PageSize).ToList();
        }
    }
}
