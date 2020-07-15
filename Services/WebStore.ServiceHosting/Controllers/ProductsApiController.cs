using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
   // [Route("api/[controller]")]
    [Route(WebApi.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("brand/{Id}")]
        public Brand GetBrand(int Id) => _ProductData.GetBrand(Id);

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands() => _ProductData.GetBrands();

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => _ProductData.GetProductById(id);

        [HttpPost]
        public PageProductsDTO GetProducts([FromBody]ProductFilter filter = null) => _ProductData.GetProducts(filter);

        [HttpGet("section/{Id}")]
        public Section GetSection(int Id) => _ProductData.GetSection(Id);

        [HttpGet("sections")]
        public IEnumerable<Section> GetSections() => _ProductData.GetSections();
    }
}
