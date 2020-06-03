﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Infrustructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _IProductData;

        public CatalogController(IProductData IProductData)
        {
            _IProductData = IProductData;
        }

        public IActionResult Shop(int? SectionId, int? BrandId)
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
                Products = products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price
                }
             ).OrderBy(p => p.Order)
            }) ;
        }
           
    }
}
