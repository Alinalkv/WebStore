using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Products.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        //include добавляют через join данные в выборку
        public Product GetProductById(int id) => _db.Products
            .Include(p => p.Brand)
            .Include(p => p.Section)
            .FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
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
            return query;
        }

        public IEnumerable<Section> GetSections() => _db.Sections;
    }
}
