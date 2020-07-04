using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>
    /// Управление сотрудниками магазина
    /// </summary>
    //[Route("api/[controller]")]
    //[Route("api/employees")]
    [Route(WebApi.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesApiController> _Logger;

        public EmployeesApiController(IEmployeesData EmployeesData, ILogger<EmployeesApiController> Logger) 
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        } 

        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        [HttpGet]
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        /// <summary>
        /// Получить сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Сотрудник</returns>
        [HttpGet("{id}")]
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        //HttpPost - создаёт внутри логики сервиса новые данные
        //FromBody - employee будет передаваться в теле запроса
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник, которого нужно добавить</param>
        /// <returns>Идентификатор сотрудника</returns>
        [HttpPost]
        public int Add([FromBody] Employee employee)
        {
            _Logger.LogInformation("Добавление нового сотрудника: [{0}] {1} {2} {3}",
                employee.Id, employee.Surname, employee.FirstName, employee.SecondName);
            var id = _EmployeesData.Add(employee);
            SaveChanges();
            return id;
        }

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник, которого нужно отредактировать</param>
        [HttpPut]
        public void Edit(Employee employee)
        {
            _Logger.LogInformation("Редактирование сотрудника: [{0}] {1} {2} {3}",
                employee.Id, employee.Surname, employee.FirstName, employee.SecondName);
            _EmployeesData.Edit(employee);
            SaveChanges();
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Идентификатор сотрудника, которого нужно удалить</param>
        /// <returns>Результат удаления сотрудника</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _Logger.LogInformation("Удаление сотрудника с id: [{0}]", id);
            var success = _EmployeesData.Delete(id);
            SaveChanges();
            return success;
        }
        
        /// <summary>
        /// Сохранение изменений
        /// </summary>
        [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();

    }
}
