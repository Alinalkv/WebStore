using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Order;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;
        public OrdersApiController(IOrderService OrderService) => _OrderService = OrderService;

        [HttpPost("{UserName}")]
        public Task<OrderDTO> CreateOrder(string UserName, [FromBody] CreateOrderModel OrderModel) => _OrderService.CreateOrder(UserName, OrderModel);

        [HttpGet("{id}")]
        public Task<OrderDTO> GetOrderById(int id) => _OrderService.GetOrderById(id);

        [HttpGet("user/{UserName}")]
        public Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => _OrderService.GetUserOrders(UserName);
    }
}
