using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration Configuration) : base(Configuration, WebApi.Products) { }

        public Brand GetBrand(int Id) => Get<Brand>($"{_ServiceAddress}/brand/{Id}");

        /// <summary>
        /// Получаем бренды
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Brand> GetBrands() => Get<IEnumerable<Brand>>($"{_ServiceAddress}/brands");

        /// <summary>
        /// Получение продукта по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{_ServiceAddress}/{id}");

        /// <summary>
        /// Получаем продукты по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public PageProductsDTO GetProducts(ProductFilter filter = null) =>
            Post(_ServiceAddress, filter ?? new ProductFilter())
            .Content
            .ReadAsAsync<PageProductsDTO>()
            .Result;

        public Section GetSection(int Id) => Get<Section>($"{_ServiceAddress}/section/{Id}");

        /// <summary>
        /// Получаем секции
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Section> GetSections() => Get<IEnumerable<Section>>($"{_ServiceAddress}/sections");
    }
}
