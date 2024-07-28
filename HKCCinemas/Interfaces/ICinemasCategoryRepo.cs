using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICinemasCategoryRepo
    {
        List<CinemasCategoryViewDTO> GetCinemasCategories();
        CinemasCategory GetCinemasCategory(int id);
        Task<bool> CreateCinemasCategory(CinemasCategoryDTO category);
        bool DeleteCinemasCategory(int id);
        Task<bool> UpdateCategoryCinemas(int id, CinemasCategoryDTO category);
        List<CinemasCategoryViewDTO> Search(QueryObject query);
        int Count();
    }
}
