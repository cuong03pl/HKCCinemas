using AutoMapper;
using HKCCinemas.DTO;
using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.EntityFrameworkCore;

namespace HKCCinemas.Repo
{
    public class FavouriteRepo : IFavouriteRepo
    {
        private readonly CinemasContext _context;
        private readonly IMapper _mapper;
        public FavouriteRepo(CinemasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool createFavourite(FavouriteDTO comment)
        {
            var favouriteMapper = _mapper.Map<Favourite>(comment);
            _context.Favourites.Add(favouriteMapper);
            _context.SaveChanges();
            return true;
        }

        public bool deleteFavourite(int filmId, string userId)
        {
            var favourite = _context.Favourites.Where(c => c.FilmId == filmId && c.UserID == userId).FirstOrDefault();
            if (favourite != null)
            {
                _context.Favourites.Remove(favourite);
                _context.SaveChanges();
                return true;
            }
            else { return false; }
        }

        public List<Favourite> GetFavourites()
        {
            return _context.Favourites.ToList();
        }

        public List<FilmDTO> getFavouritesByUserId(string userId)
        {
           var data = _context.Favourites.Where(f => f.UserID == userId).Include(f => f.Film)
                .Select(f => f.Film).ToList();
            
            return _mapper.Map<List<FilmDTO>>(data);
        }

        public bool isFavourited(int filmId, string userId)
        {
            var data = _context.Favourites.Where(f => f.UserID == userId && f.FilmId == filmId).ToList();
            return data.Any();
        }
    }
}
