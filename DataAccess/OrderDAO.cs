using BusinessObjects;
using System.Linq.Expressions;

namespace DataAccess
{
    public class OrderDAO : BaseDAO<Order>
    {
        public OrderDAO(MyDBContext context) : base(context) { }
        public List<Order> GetOrders(
             string? customerId = null,
             int pageIndex = 1,
             int pageSize = 5,
             string orderBy = "OrderDate")
        {
            // Define filter condition
            Expression<Func<Order, bool>> filter = o =>
                (string.IsNullOrEmpty(customerId) || o.CustomerID == customerId);

            // Define ordering expression
            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderByExpression = q => orderBy.ToLower() switch
            {
                "orderdatedesc" => q.OrderByDescending(o => o.OrderDate),
                _ => q.OrderBy(o => o.OrderDate) // Default sorting by OrderDate
            };

            // Call the Get method from BaseDAO with filtering, ordering, and pagination
            return Get(
                filter: filter,
                orderBy: orderByExpression,
                pageIndex: pageIndex,
                pageSize: pageSize,
                includeProperties: "OrderDetails" // Include related OrderDetails if needed
            ).ToList();
        }

        public Order FindOrderById(int orderId)
        {
            return GetByID(orderId);
        }

        public Order SaveOrder(Order order)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void UpdateOrder(Order order)
        {
            Update(order);
            _dbContext.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
            _dbContext.SaveChanges();
        }
    }
}
