using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICategoryRepo
    {
        List<Category> GetAllCategoriesByFilmId(int film_id);
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int id);
        int CountCategory();
    }
}
