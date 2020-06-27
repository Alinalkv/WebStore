using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities.Orders;

namespace WebStore.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderDTO ToDTO(this Order o) => o is null ? null : new OrderDTO
        {
             Address = o.Address,
             Date = o.Date,
             Id = o.Id,
             Items = o.Items.Select(ToDTO),
             Name = o.Name,
             Phone = o.Phone,
        };
        public static Order FromDTO(this OrderDTO o) => o is null ? null : new Order
        {
             Address = o.Address,
             Phone = o.Phone,
             Date = o.Date,
             Id = o.Id,
             Items = o.Items.Select(FromDTO).ToArray(),
             Name = o.Name,
              
        };

        public static OrderItemDTO ToDTO(this OrderItem o) => o is null ? null : new OrderItemDTO
        {
             Id = o.Id,
              Price = o.Price,
               Quantity = o.Quantity,
        };

        public static OrderItem FromDTO(this OrderItemDTO o) => o is null ? null : new OrderItem
        {
            Id = o.Id,
            Price = o.Price,
            Quantity = o.Quantity,
        };
    }
}
