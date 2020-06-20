using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrustructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) =>  _ProductData = ProductData;
        public IActionResult Index() => View(_ProductData.GetProducts());
    }
}
