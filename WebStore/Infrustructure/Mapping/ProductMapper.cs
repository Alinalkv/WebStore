using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrustructure.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product) => new ProductViewModel { 
        
            Id = product.Id,
            ImageUrl = product.ImageUrl,
            Name = product.Name,
            Order = product.Order, 
            Price = product.Price,
            Brand = product.Brand?.Name,
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products) => products.Select(ToView);
    }
}
