using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Infrustructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infrustructure.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;

        public SqlOrderService(WebStoreDB db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }
        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user is null)
                throw new InvalidOperationException($"Пользователь {UserName} не найден");

            await using var transaction = await _db.Database.BeginTransactionAsync();
            var order = new Order
            {
                Address = OrderModel.Address,
                User = user,
                Date = DateTime.Now,
                Name = OrderModel.Name,
                Phone = OrderModel.Phone,
                Items = new List<OrderItem>()
            };

            foreach (var (product_model, quantity) in Cart.Items)
            {
                var product = await _db.Products.FindAsync(product_model.Id);
                if(product is null)
                    throw new InvalidOperationException($"Товар Id:{product_model.Id} не найден");
                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Product = product,
                    Quantity = quantity
                };

                order.Items.Add(order_item);
            }
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order;
        }

        public async Task<Order> GetOrderById(int id) => await _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IEnumerable<Order>> GetUserOrders(string UserName) => await _db.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .Where(o => o.User.UserName == UserName)
            .ToArrayAsync();
    }
}
