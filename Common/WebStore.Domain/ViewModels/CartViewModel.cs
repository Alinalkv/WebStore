using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Domain.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel product, int Quantity)> Items { get; set; }
        public int ItemsCount => Items?.Sum(p => p.Quantity) ?? 0;
    }
}
