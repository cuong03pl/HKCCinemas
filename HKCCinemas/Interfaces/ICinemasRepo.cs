using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICinemasRepo
    {
        List<CinemasViewDTO> GetAllCinemas();
        List<Cinemas> GetCinemasByCategoryId(int cinemasId);
        Cinemas GetCinemasById(int id);
        Task<bool> CreateCinemasAsync(CinemasDTO cinemas);
        Task<bool> UpdateCinemas(int id, CinemasDTO cinemas);
        bool DeleteCinemas(int id);
        int Count();

        List<CinemasViewDTO> SearchCinemas(QueryObject query);

    }
}
