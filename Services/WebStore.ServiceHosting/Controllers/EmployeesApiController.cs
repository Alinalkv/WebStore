using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    //[Route("api/employees")]
    [Route(WebApi.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        //HttpGet - получение данных
        [HttpGet]
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{id}")]
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        //HttpPost - создаёт внутри логики сервиса новые данные
        //FromBody - employee будет передаваться в теле запроса
        [HttpPost]
        public int Add([FromBody] Employee employee)
        {
            var id = _EmployeesData.Add(employee);
            SaveChanges();
            return id;
        }

        [HttpPut]
        public void Edit(Employee employee)
        {
            _EmployeesData.Edit(employee);
            SaveChanges();
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
           var success = _EmployeesData.Delete(id);
            SaveChanges();
            return success;
        }
        
        [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();

    }
}
