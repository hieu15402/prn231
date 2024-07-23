using BusinessObjects;
using DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Repositories.impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyDBContext _context;
        private readonly OrderDAO _orderDAO;
        private readonly FlowerBouquetDAO _flowerBouquetDAO;
        public OrderRepository()
        {
            _context = new MyDBContext();
            _orderDAO = new OrderDAO(_context);
            _flowerBouquetDAO = new FlowerBouquetDAO(_context);
        }
        public Order SaveOrder(Order order) => _orderDAO.SaveOrder(order);
        public Order GetOrderById(int id) => _orderDAO.FindOrderById(id);
        public List<Order> GetOrders(string? customerId = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "OrderDate") => _orderDAO.GetOrders(customerId: customerId,
                pageIndex: pageIndex,
                pageSize: pageSize,
                orderBy: orderBy);
        public void UpdateOrder(Order order) => _orderDAO.UpdateOrder(order);
        public void DeleteOrder(Order order) => _orderDAO.DeleteOrder(order);
        public Order? PutOrderShipped(int id)
        {
            var oTmp = _orderDAO.FindOrderById(id);
            if (oTmp == null)
            {
                return null;
            }
            oTmp.ShippedDate = DateTime.Now;
            oTmp.OrderStatus = 1;
            _orderDAO.UpdateOrder(oTmp);
            return oTmp;
        }
        public Order? PutOrderCancel(int id)
        {
            var oTmp = _orderDAO.FindOrderById(id);
            if (oTmp == null)
            {
                return null;
            }
            oTmp.OrderStatus = 2;
            _orderDAO.UpdateOrder(oTmp);
            var orderDetails = OrderDetailDAO.FindAllOrderDetailsByOrderId(id);
            foreach (var od in orderDetails)
            {
                var fb = _flowerBouquetDAO.GetByID(od.FlowerBouquetID);
                fb.UnitsInStock += od.Quantity;
                _flowerBouquetDAO.UpdateFlowerBouquet(fb);
            }
            return oTmp;
        }
    }
}
