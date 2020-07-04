using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Products.InSQL
{
    public class SqlEmployeeData : IEmployeesData
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<SqlEmployeeData> _Logger;

        public SqlEmployeeData(WebStoreDB db, ILogger<SqlEmployeeData> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public int Add(Employee employee)
        {
            if (employee is null)
            {
                _Logger.LogError("Не указан сотрудник, которого нуно добавить");
                throw new ArgumentNullException(nameof(employee));
            }
                
            if (employee.Id != 0)
            {
                _Logger.LogError("Для добавления сотрудника [{0}] {1} {2} {3} вручную задан первичный ключ", employee.Id, employee.Surname, employee.FirstName, employee.SecondName);
                throw new InvalidOperationException("Для добавления сотрудника вручную задан первичный ключ");
            }
            _db.Employees.Add(employee);
            _Logger.LogInformation("Новый сотрудник добавлен: [{0}] {1} {2} {3}",
               employee.Id, employee.Surname, employee.FirstName, employee.SecondName);
            return employee.Id;
        }

        public bool Delete(int id)
        {
            var db_employee = GetById(id);
            if (db_employee is null)
            {
                _Logger.LogError("Не указан сотрудник, которого нуно удалить");
                return false;
            }
            _db.Employees.Remove(db_employee);
            _Logger.LogInformation("Сотрудник с id [{0}] удалён",
               id);
            return true;

            //вариант 2
            //_db.Remove(db_employee);
        }

        public void Edit(Employee employee)
        {
            //вариант 1
            if (employee is null)
            {
                _Logger.LogError("Не указан сотрудник, которого нуно редактировать");
                throw new ArgumentNullException(nameof(employee));
            }
                
            var db_employee = GetById(employee.Id);
            if (db_employee is null)
            {
                _Logger.LogInformation("Cотрудника [{0}] {1} {2} {3} нет в базе для удаления",
                    employee.Id, employee.Surname, employee.FirstName, employee.SecondName);
                return;
            }
            

            db_employee.FirstName = employee.FirstName;
            db_employee.SecondName = employee.SecondName;
            db_employee.Surname = employee.Surname;
            db_employee.Age = employee.Age;

            //вариант 2
            //_db.Update(employee);
        }

        public IEnumerable<Employee> Get() => _db.Employees;

        public Employee GetById(int id) => _db.Employees.FirstOrDefault(e => e.Id == id);

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
