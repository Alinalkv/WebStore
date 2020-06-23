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

        public SqlEmployeeData(WebStoreDB db)
        {
            _db = db;
        }

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));
            if (employee.Id != 0)
                throw new InvalidOperationException("Для добавления сотрудника вручную задан первичный ключ");
            _db.Employees.Add(employee);
            return employee.Id;
        }

        public bool Delete(int id)
        {
            var db_employee = GetById(id);
            if (db_employee is null)
                return false;
            _db.Employees.Remove(db_employee);
            return true;

            //вариант 2
            //_db.Remove(db_employee);
        }

        public void Edit(Employee employee)
        {
            //вариант 1
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));
            var db_employee = GetById(employee.Id);
            if (db_employee is null) return;

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
