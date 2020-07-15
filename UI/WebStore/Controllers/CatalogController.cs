using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _IProductData;
        private readonly IConfiguration _Configuration;

        public CatalogController(IProductData IProductData, IConfiguration Configuration)
        {
            _IProductData = IProductData;
            _Configuration = Configuration;
        }

        public IActionResult Shop(int? SectionId, int? BrandId, [FromServices] IMapper Mapper, int Page = 1)
        {

            var page_size = int.TryParse(_Configuration["PageSize"], out var size) ? size : (int?)null;
            
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Page = Page,
                PageSize = page_size
            };

            var products = _IProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = products
                .Products
                .Select(p => p.FromDTO())
                .Select(Mapper.Map<ProductViewModel>)
                .OrderBy(p => p.Order),
                 PageViewModel = new PageViewModel { 
                    PageSize = page_size ?? 0,
                    PageNumber = Page,
                    TotalItems = products.TotalCount
                 }
            }) ;
        }
        

        /// <summary>
        /// Возвращает подробную инфу о товаре
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var product = _IProductData.GetProductById(id);
            if (product is null)
                return NotFound();
            return View(product.FromDTO().ToView());
        }
    }
}
