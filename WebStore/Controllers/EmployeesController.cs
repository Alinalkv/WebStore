using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {

        private static readonly List<Employee> _Employees = TestData.Employees;
        public IActionResult Index()
        {
            return View(_Employees);
        }

        public IActionResult EmployeeDetails(int id)
        {
            var employee = _Employees.FirstOrDefault(c => c.Id == id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }
    }
}
