using BusinessObjects;

namespace Repositories
{
    public interface ICategoryRepository
    {
        void SaveCategory(Category category);
        Category GetCategoryById(int id);
        List<Category> GetCategories(
        string? keyword = null,
        int pageIndex = 1,
        int pageSize = 5,
        string orderBy = "CategoryName");
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
