using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;

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

        // get
        public List<Trailer> GetAllTrailer()
        {
            return _context.Trailers.ToList();
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
    }
}
