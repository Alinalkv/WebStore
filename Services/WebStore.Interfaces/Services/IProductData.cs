using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        Section GetSection(int Id);

        IEnumerable<Brand> GetBrands();

        Brand GetBrand(int Id);

        IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null);
        ProductDTO GetProductById(int id);
    }
}
