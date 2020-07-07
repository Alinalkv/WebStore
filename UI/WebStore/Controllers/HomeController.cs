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


    }
}
