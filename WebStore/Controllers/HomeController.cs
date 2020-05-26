using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _Employees = new List<Employee> {

            new Employee
            {
                Id = 1,
                FirstName = "Иван",
                SecondName = "Иванович",
                Surname = "Иванов",
                Age = 28
            },
            new Employee
            {
                Id = 2,
                FirstName = "Пётр",
                SecondName = "Петрович",
                Surname = "Петров",
                Age = 29
            },
            new Employee
            {
                Id = 3,
                FirstName = "Алесандр",
                SecondName = "Алесандрович",
                Surname = "Алесандров",
                Age = 28
            },
            };



        public IActionResult Index() => View();
 

        public IActionResult EmployeeDetails (int id)
        {
            var employee = _Employees.FirstOrDefault(c => c.Id == id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        public IActionResult AnotherAction()
        {
            return Content("First controller second action");
        }
    }
}
