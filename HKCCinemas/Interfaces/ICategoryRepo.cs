using HKCCinemas.DTO;
using HKCCinemas.Helper;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICategoryRepo
    {
        List<Category> GetAllCategoriesByFilmId(int film_id);
        List<int> GetAllCategoryIdsByFilmId(int film_id);
        List<CategoryViewDTO> GetAllCategories();
        Category GetCategoryById(int id);
        bool CreateCategory(CategoryViewDTO category);
        bool UpdateCategory(int id, CategoryViewDTO category);
        bool DeleteCategory(int id);

        int Count();
        List<CategoryViewDTO> SearchCategory(QueryObject query);
    }
}
