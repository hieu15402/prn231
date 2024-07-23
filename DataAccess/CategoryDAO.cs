using BusinessObjects;
using System.Linq.Expressions;

namespace DataAccess
{
    public class CategoryDAO : BaseDAO<Category>
    {
        public CategoryDAO(MyDBContext dbContext) : base(dbContext)
        {
        }
        public List<Category> GetCategories(
        string? keyword = null,
        int pageIndex = 1,
        int pageSize = 5,
        string orderBy = "CategoryName")
        {
            // Xác định điều kiện lọc
            Expression<Func<Category, bool>> filter = c =>
                string.IsNullOrEmpty(keyword) || c.CategoryName.Contains(keyword);

            // Xác định phương thức sắp xếp
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderByExpression = q => orderBy.ToLower() switch
            {
                "categorynamedesc" => q.OrderByDescending(c => c.CategoryName),
                _ => q.OrderBy(c => c.CategoryName) // Sắp xếp theo CategoryName theo mặc định
            };
            // Gọi phương thức Get từ BaseDAO
            return Get(
                filter: filter,
                orderBy: orderByExpression,
                pageIndex: pageIndex,
                pageSize: pageSize
            ).ToList();
        }

        public Category FindCategoryById(int categoryID)
        {
            return GetByID(categoryID);
        }

        public void SaveCategory(Category category)
        {
            Insert(category);
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            Update(category);
            _dbContext.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
            _dbContext.SaveChanges();
        }
    }
}