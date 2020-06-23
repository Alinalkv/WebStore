using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels
{
    public class UserOrderViewModel
    {
        //id пользователя
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        //сумма заказа
        public decimal TotalSum { get; set; }
    }
}
