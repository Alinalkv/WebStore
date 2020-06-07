using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrustructure.Interfaces;

namespace WebStore.Infrustructure.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IEnumerable <Product> query = _db.Products;
            if (filter?.BrandId != null)
                query = query.Where(c => c.BrandId == filter.BrandId);

            if (filter?.SectionId != null)
                query = query.Where(c => c.SectionId == filter.SectionId);

            return query;
        }

        public IEnumerable<Section> GetSections() => _db.Sections;
    }
}
