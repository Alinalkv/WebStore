using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) =>  _ProductData = ProductData;
        public IActionResult Index() => View(_ProductData.GetProducts().Select(p => p.FromDTO()));

        public IActionResult Edit(int? id)
        {
            var product = id is null ? new Product() : _ProductData.GetProductById((int)id).FromDTO();
            if (product is null)
                return NotFound();
            return View(product);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            //if (product is null)
            //    throw new ArgumentNullException(nameof(product));

            //if (!ModelState.IsValid)
            //    return View(product);

            //var new_product = new Product
            //{
            //    Id = product.Id,
            //    Brand = product.Brand,
            //    BrandId = product.BrandId,
            //    ImageUrl = product.ImageUrl,
            //    Name = product.Name,
            //    Order = product.Order,
            //    Price = product.Price,
            //    Section = product.Section,
            //    SectionId = product.SectionId
            //};

            //if(new_product.Id == 0)
            //{
            //    _ProductData.
            //}
            return RedirectToAction(nameof(Index));
           }

        public IActionResult Delete(int id)
        {
            var product = _ProductData.GetProductById(id);
            if (product is null)
                return NotFound();
            return View(product.FromDTO());
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName(nameof(Delete))]
        public IActionResult DeleteConfirm(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
