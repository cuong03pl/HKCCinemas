using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICinemasRepo
    {
        List<Cinemas> GetAllCinemas();
        List<Cinemas> GetCinemasByCategoryId(int cinemasId);
        Cinemas GetCinemasById(int id);
        Task<bool> CreateCinemasAsync(CinemasDTO cinemas);
        Task<bool> UpdateCinemas(int id, CinemasDTO cinemas);
        bool DeleteCinemas(int id);
        int CountCinemas();
        List<Cinemas> SearchCinemas(string keyword);

    }
}
