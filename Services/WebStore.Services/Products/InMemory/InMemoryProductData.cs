using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.Products.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            var products = TestData.Products;

            if (filter?.SectionId != null)
            {
                products = products.Where(prod => prod.SectionId == filter.SectionId);
            }

            if (filter?.BrandId != null)
            {
                products = products.Where(prod => prod.BrandId == filter.BrandId);
            }

            return products;
        }



        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
