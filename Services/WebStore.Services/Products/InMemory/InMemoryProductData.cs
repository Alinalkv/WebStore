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
        public Brand GetBrand(int Id) => TestData.Brands.FirstOrDefault(b => b.Id == Id);

        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public ProductDTO GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id).ToDTO();

        public PageProductsDTO GetProducts(ProductFilter filter = null)
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

            var total_count = products.Count();
            if (filter?.PageSize > 0)
            {
                products = products
                    .Skip((filter.Page - 1) * (int)filter.PageSize)
                    .Take((int)filter.PageSize);
            }
            return new PageProductsDTO
            {
                Products = products.Select(p => p.ToDTO()),
                TotalCount = total_count
            };
        }

        public Section GetSection(int Id) => TestData.Sections.FirstOrDefault(b => b.Id == Id);

        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
