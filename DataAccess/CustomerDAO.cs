using BusinessObjects;
using System.Linq.Expressions;

namespace DataAccess
{
    public class CustomerDAO : BaseDAO<Customer>
    {
        public CustomerDAO(MyDBContext context) : base(context)
    {
    }
        public List<Customer> GetCustomers(
        string? keyword = null,
        int pageIndex = 1,
        int pageSize = 5,
        string orderBy = "CustomerName")
        {
            // Xác định điều kiện lọc
            Expression<Func<Customer, bool>> filter = c =>
                string.IsNullOrEmpty(keyword) || c.CustomerName.Contains(keyword);

            // Xác định phương thức sắp xếp
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderByExpression = q => orderBy.ToLower() switch
            {
                "customername" => q.OrderBy(c => c.CustomerName),
                "customernamedesc" => q.OrderByDescending(c => c.CustomerName),
                _ => q.OrderBy(c => c.CustomerName) // Sắp xếp theo CustomerName theo mặc định
            };

            // Gọi phương thức Get từ BaseDAO
            return Get(
                filter: filter,
                orderBy: orderByExpression,
                pageIndex: pageIndex,
                pageSize: pageSize
            ).ToList();
        }
        public Customer FindCustomerById(string customerId)
        {
            return GetByID(customerId);
        }
        public Customer FindCustomerByEmail(string email)
        {
            return Get(c => c.Email == email).FirstOrDefault();
        }
        public void SaveCustomer(Customer customer)
        {
            Insert(customer);
            _dbContext.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            Update(customer);
            _dbContext.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            Delete(customer);
            _dbContext.SaveChanges();
        }
    }
}