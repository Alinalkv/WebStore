using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger<EmployeesClient> _Logger;

        public EmployeesClient(IConfiguration Configuration, ILogger<EmployeesClient> Logger) : base(Configuration, WebApi.Employees)
        {
            _Logger = Logger;
        }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(_ServiceAddress);

        public Employee GetById(int id) => Get<Employee>($"{_ServiceAddress}/{id}");

        //так как надо получить id
        public int Add(Employee employee)
        {
            try
            {
                _Logger.LogInformation("Обращение к {0} для добавления сотрудника с id {1}", _ServiceAddress, employee.Id);
                return Post<Employee>(_ServiceAddress, employee).Content.ReadAsAsync<int>().Result;
            }
            catch (Exception error)
            {
                _Logger.LogError("Ошика при обращении к {0} для добавления сотрудника с id {1}: {2}", _ServiceAddress, employee.Id, error);
                throw new InvalidOperationException($"Ошика при обращении к {_ServiceAddress} для добавления сотрудника с id {employee.Id}", error);
            }
            
        }

        public void Edit(Employee employee)
        {
            try
            {
                _Logger.LogInformation("Обращение к {0} для редактирования сотрудника с id {1}", _ServiceAddress, employee.Id);
                Put<Employee>(_ServiceAddress, employee);
            }
            catch (Exception error)
            {
                _Logger.LogError("Ошика при обращении к {0} для редактирования сотрудника с id {1}: {2}", _ServiceAddress, employee.Id, error);
                throw new InvalidOperationException($"Ошика при обращении к {_ServiceAddress} для редактирования сотрудника с id {employee.Id}", error);
            }
            
        }

        public bool Delete(int id)
        {
            try
            {
                _Logger.LogInformation("Обращение к {0} для удаления сотрудника с id {1}", _ServiceAddress, id);
                return Delete($"{_ServiceAddress}/{id}").IsSuccessStatusCode;
            }
            catch (Exception error)
            {
                _Logger.LogError("Ошика при обращении к {0} для редактирования сотрудника с id {1}: {2}", _ServiceAddress, id, error);
                throw new InvalidOperationException($"Ошика при обращении к {_ServiceAddress} для редактирования сотрудника с id {id}", error);
            }
            
        }

        public void SaveChanges() { }
    }
}
