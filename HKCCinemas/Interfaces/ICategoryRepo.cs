using HKCCinemas.DTO;
using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICategoryRepo
    {
        List<Category> GetAllCategoriesByFilmId(int film_id);
        List<int> GetAllCategoryIdsByFilmId(int film_id);
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        bool CreateCategory(CategoryDTO category);
        bool UpdateCategory(int id, CategoryDTO category);
        bool DeleteCategory(int id);
        int CountCategory();

        List<Category> SearchCategory(string keyword);
    }
}
