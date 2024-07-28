using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ITrailerRepo
    {
        List<Trailer> GetAllTrailerByFilmId(int film_id);
        List<TrailerViewDTO> GetAllTrailer();
        Task<bool> CreateTrailerAsync(TrailerDTO trailer);
        Task<bool> UpdateTrailerAsync(int id, TrailerDTO trailer);
        bool DeleteTrailer(int trailer_id);
        List<TrailerViewDTO> Search(QueryObject query);
        int Count();


    }
}
