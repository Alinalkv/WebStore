using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public Brand GetBrand(int Id) => _db.Brands.Find(Id);

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        //include добавляют через join данные в выборку
        public ProductDTO GetProductById(int id) => _db.Products
            .Include(p => p.Brand)
            .Include(p => p.Section)
            .FirstOrDefault(p => p.Id == id)
            .ToDTO();

        public PageProductsDTO GetProducts(ProductFilter filter = null)
        {
            IEnumerable<Product> query = _db.Products;

            if (filter?.Ids?.Length > 0)
                query = query.Where(c => filter.Ids.Contains(c.Id));
            else
            {
                if (filter?.BrandId != null)
                    query = query.Where(c => c.BrandId == filter.BrandId);

                if (filter?.SectionId != null)
                    query = query.Where(c => c.SectionId == filter.SectionId);
            }

            var total_count = query.Count();
            if(filter?.PageSize > 0)
            {
                query = query
                    .Skip((filter.Page - 1) * (int)filter.PageSize)
                    .Take((int)filter.PageSize);
            }
            return new PageProductsDTO {
             Products = query.Select(p => p.ToDTO()),
              TotalCount = total_count
            };
        }

        public Section GetSection(int Id) => _db.Sections.Include(p => p.ParentSection).FirstOrDefault(s => s.Id == Id);

        public IEnumerable<Section> GetSections() => _db.Sections;
    }
}
