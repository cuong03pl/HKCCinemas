using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ICategoryRepo
    {
        List<Category> GetAllCategories(int film_id);
        Category GetCategoryById(int id);
        bool CreateCategory(int filmId, Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int id);
        int CountCategory();
    }
}
