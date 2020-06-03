using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrustructure.Interfaces;

namespace WebStore.Infrustructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

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
