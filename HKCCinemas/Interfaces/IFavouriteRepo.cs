using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface IFavouriteRepo
    {
        public List<Favourite> GetFavourites();
        public List<FilmDTO> getFavouritesByUserId(string userId);
        bool createFavourite(FavouriteDTO comment);
        bool deleteFavourite(int filmId, string userId);
        bool isFavourited (int filmId, string userId);

        int Count(string userId);
    }
}
