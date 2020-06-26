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
        public int Add([FromBody] Employee employee) => _EmployeesData.Add(employee);

        [HttpPut]
        public void Edit(Employee employee) => _EmployeesData.Edit(employee);

        [HttpDelete("{id}")]
        public bool Delete(int id) => _EmployeesData.Delete(id);
        
        public void SaveChanges() => _EmployeesData.SaveChanges();

    }
}
