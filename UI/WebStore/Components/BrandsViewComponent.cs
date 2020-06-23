using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Infrustructure.Interfaces;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public BrandsViewComponent(IProductData ProductData)
        {
            _ProductData = ProductData;
        }

        //во вью компоненте дб invoke
        //делаем какую-то логику и возвращаем представление
        public IViewComponentResult Invoke() => View(GetBrands());

        private IEnumerable<BrandViewModel> GetBrands()
        {
            var brands = _ProductData.GetBrands();
            var brand_views = brands.Select(b => new BrandViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order
            }).ToList();

            brand_views.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));
            return brand_views;
        }
    }
}
