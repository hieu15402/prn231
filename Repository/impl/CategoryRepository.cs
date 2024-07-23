using BusinessObjects;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Repositories.impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDBContext _context;
        private readonly CategoryDAO _categoryDAO;

        public CategoryRepository()
        {
            _context = new MyDBContext();
            _categoryDAO = new CategoryDAO(_context);
        }


        public void SaveCategory(Category category) => _categoryDAO.SaveCategory(category);
        public Category GetCategoryById(int id) => _categoryDAO.FindCategoryById(id);
        public List<Category> GetCategories(
        string? keyword = null,
        int pageIndex = 1,
        int pageSize = 5,
        string orderBy = "CategoryName")
        {
            return _categoryDAO.GetCategories(keyword, pageIndex, pageSize, orderBy);
        }
        public void UpdateCategory(Category category) => _categoryDAO.UpdateCategory(category);
        public void DeleteCategory(Category category) => _categoryDAO.DeleteCategory(category);
    }
}
