using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(string UserName, CreateOrderModel OrderModel);
        Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName);
        Task<OrderDTO> GetOrderById(int id);
    }
}
