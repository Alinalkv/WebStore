using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
 
        public IActionResult Index() => View();
        public IActionResult Error404() => View();
        public IActionResult Blog() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult ContactUs() => View();

        public IActionResult Throw(string id) => throw new ApplicationException(id ?? "Error");

        public IActionResult ErrorStatus(string Code)
        {
            switch(Code)
            {
                case "404":
                    return RedirectToAction(nameof(Error404));
                default:
                    return Content($"Error code: {Code}");
            }
                
        }


    }
}
