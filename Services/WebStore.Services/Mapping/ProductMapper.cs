using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product) => new ProductViewModel
        {

            Id = product.Id,
            ImageUrl = product.ImageUrl,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            Brand = product.Brand?.Name,
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products) => products.Select(ToView);

        public static ProductDTO ToDTO(this Product p) => p is null ? null : new ProductDTO
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Order = p.Order,
            ImageUrl = p.ImageUrl,
            Brand = p.Brand.ToDTO(),
            Section = p.Section.ToDTO(),
        };

        public static Product FromDTO(this ProductDTO p) => p is null ? null : new Product
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Order = p.Order,
            ImageUrl = p.ImageUrl,
            Brand = p.Brand.FromDTO(),
            BrandId = p.Brand?.Id,
            Section = p.Section.FromDTO(),
           // SectionId = p.Section.Id,
        };
    }

    public static class SectionMappier
    {
        public static SectionDTO ToDTO(this Section s) => s is null ? null : new SectionDTO
        {
            Id = s.Id,
            Name = s.Name,
        };

        public static Section FromDTO(this SectionDTO s) => s is null ? null : new Section
        {
            Id = s.Id,
            Name = s.Name,
        };
    }

    public static class BrandMappier
    {
        public static BrandDTO ToDTO(this Brand s) => s is null ? null : new BrandDTO
        {
            Id = s.Id,
            Name = s.Name,
        };

        public static Brand FromDTO(this BrandDTO s) => s is null ? null : new Brand
        {
            Id = s.Id,
            Name = s.Name,
        };
    }
}
