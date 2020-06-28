using System.Collections.Generic;
using System.Text;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO.Order
{
    public class CreateOrderModel
    {
        //ссылка на вью-модель заказа
        public OrderViewModel Order { get; set; }

        //пункты заказа
        public List<OrderItemDTO> Items { get; set; }
    }
}
