using BusinessObjects;

namespace Repositories
{
    public interface IOrderRepository
    {
        Order SaveOrder(Order order);
        Order GetOrderById(int id);
        List<Order> GetOrders(string? customerId = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "OrderDate");
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        Order? PutOrderShipped(int id);
        Order? PutOrderCancel(int id);
    }
}
