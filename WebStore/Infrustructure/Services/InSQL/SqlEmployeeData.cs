using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrustructure.Interfaces;

namespace WebStore.Infrustructure.Services.InSQL
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
            if (_db.Employees.Contains(employee))
                return employee.Id;

            //employee.Id = _db.Employees.Count() == 0 ? 1 : _db.Employees.Max(c => c.Id) + 1;
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
        }

        public void Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));
            //объект уже содержится в списке с изменёнными данными
            if (_db.Employees.Contains(employee))
                return;
            var db_employee = GetById(employee.Id);
            db_employee.FirstName = employee.FirstName;
            db_employee.SecondName = employee.SecondName;
            db_employee.Surname = employee.Surname;
            db_employee.Age = employee.Age;
        }

        public IEnumerable<Employee> Get() => _db.Employees;

        public Employee GetById(int id) => _db.Employees.FirstOrDefault(e => e.Id == id);

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
