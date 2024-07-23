using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICinemasCategoryRepo
    {
        List<CinemasCategory> GetCinemasCategories();
        CinemasCategory GetCinemasCategory(int id);
        Task<bool> CreateCinemasCategory(CinemasCategoryDTO category);
        bool DeleteCinemasCategory(int id);
        Task<bool> UpdateCategoryCinemas(int id, CinemasCategoryDTO category);
        List<CinemasCategory> Search(string keyword);
    }
}
