using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _IProductData;

        public CatalogController(IProductData IProductData)
        {
            _IProductData = IProductData;
        }

        public IActionResult Shop(int? SectionId, int? BrandId, [FromServices] IMapper Mapper)
        {
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId
            };

            var products = _IProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = products
                .Select(p => p.FromDTO())
                .Select(Mapper.Map<ProductViewModel>)
               // .ToView()
                .OrderBy(p => p.Order)
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
