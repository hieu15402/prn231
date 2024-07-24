using BusinessObjects;
using FlowerBouquetWebAPI.BusinessObjects;
using FlowerBouquetWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace FlowerBouquetWebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository repository = new OrderRepository();
        private IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        private IFlowerBouquetRepository flowerBouquetRepository = new FlowerBouquetRepository();

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders(
            string? customerId = null,
            int pageIndex = 1,
            int pageSize = 5,
            string orderBy = "OrderDate") => Ok(new ResponseObject<IEnumerable<Order>>("Get success", repository.GetOrders(
                customerId: customerId,
                pageIndex: pageIndex,
                pageSize: pageSize,
                orderBy: orderBy)));

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var order = repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound(new ResponseObject<String>("Not found", ""));
            }
            return Ok(new ResponseObject<Order>("Get success", order));
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public ActionResult<Order> PostOrder(PostOrder postOrder)
        {
            var total = 0;
            foreach (var od in postOrder.OrderDetails)
            {
                var fb = flowerBouquetRepository.GetFlowerBouquetById(od.FlowerBouquetID);
                if (fb == null)
                {
                    return NotFound(new ResponseObject<String>("Not found", ""));
                }
                if (fb.FlowerBouquetStatus != 1)
                {
                    return BadRequest();
                }
                if (fb.UnitsInStock < od.Quantity)
                {
                    return BadRequest();
                }
            }
            foreach (var od in postOrder.OrderDetails)
            {
                var fb = flowerBouquetRepository.GetFlowerBouquetById(od.FlowerBouquetID);
                total += fb.UnitPrice * od.Quantity;
            }
            var order = new Order
            {
                OrderDate = postOrder.OrderDate,
                ShippedDate = null,
                Total = total,
                OrderStatus = 0,
                Freight = postOrder.Freight,
                CustomerID = postOrder.CustomerID
            };
            var savedOrder = repository.SaveOrder(order);
            foreach (var od in postOrder.OrderDetails)
            {
                var fb = flowerBouquetRepository.GetFlowerBouquetById(od.FlowerBouquetID);
                fb.UnitsInStock -= od.Quantity;
                var orderDetail = new OrderDetail
                {
                    FlowerBouquetID = od.FlowerBouquetID,
                    UnitPrice = fb.UnitPrice,
                    Quantity = od.Quantity,
                    OrderID = savedOrder.OrderID,
                    Discount = 0
                };
                flowerBouquetRepository.UpdateFlowerBouquet(fb);
                orderDetailRepository.SaveOrderDetail(orderDetail);
            }
            return Ok(new ResponseObject<Order>("Create success", order));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("shipped/{id}")]
        public IActionResult PutOrderShipped(int id)
        {
            var oTmp = repository.PutOrderShipped(id);
            if (oTmp == null)
            {
                return NotFound(new ResponseObject<String>("Not found", ""));
            }
            if (oTmp.OrderStatus == 0)
            {
                return BadRequest();
            }
            return Ok(new ResponseObject<Order>("Update success", oTmp));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("cancel/{id}")]
        public IActionResult PutOrderCancel(int id)
        {
            var oTmp = repository.PutOrderCancel(id);
            if (oTmp == null)
            {
                return NotFound(new ResponseObject<String>("Not found", ""));
            }
            if (oTmp.OrderStatus == 0)
            {
                return BadRequest();
            }
            return Ok(new ResponseObject<Order>("Update success", oTmp));
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var oTmp = repository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound(new ResponseObject<String>("Not found", ""));
            }
            if (oTmp.OrderStatus == 0)
            {
                return BadRequest();
            }
            repository.DeleteOrder(oTmp);
            return Ok(new ResponseObject<Order>("Delete success", oTmp));
        }
    }
}
