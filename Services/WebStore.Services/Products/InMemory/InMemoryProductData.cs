using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public ProductDTO GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id).ToDTO();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null)
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

            return products.Select(p => p.ToDTO());
        }



        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
