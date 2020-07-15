using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.DTO.Products
{
    public class PageProductsDTO
    {
        public IEnumerable<ProductDTO> Products { get; set; }

        //Продукты, не вошедшие на страницу
        public int TotalCount { get; set; }
    }
}
