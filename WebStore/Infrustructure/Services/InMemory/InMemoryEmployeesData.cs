using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrustructure.Interfaces;

namespace WebStore.Infrustructure.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _Employees = TestData.Employees;
        
        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));
            if (_Employees.Contains(employee))
                return employee.Id;

            employee.Id = _Employees.Count == 0 ? 1 : _Employees.Max(c => c.Id) + 1;
            TestData.Employees.Add(employee);
            return employee.Id;
        }

        public bool Delete(int id)
        {
            var db_employee = GetById(id);
            if (db_employee is null)
                return false;
            return _Employees.Remove(db_employee);
        }

        public void Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));
            //объект уже содержится в списке с изменёнными данными
            if (_Employees.Contains(employee))
                return;
           var db_employee = GetById(employee.Id);
            db_employee.FirstName = employee.FirstName;
            db_employee.SecondName = employee.SecondName;
            db_employee.Surname = employee.Surname;
            db_employee.Age = employee.Age;
        }

        public IEnumerable<Employee> Get() => _Employees;

        public Employee GetById(int id) => _Employees.FirstOrDefault(e => e.Id == id);

        public void SaveChanges()
        {
        }
    }
}
