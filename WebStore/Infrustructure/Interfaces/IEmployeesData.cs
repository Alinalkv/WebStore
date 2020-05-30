using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infrustructure.Interfaces
{
    public interface IEmployeesData
    {
        /// <summary>
        /// Получить сотрудников
        /// </summary>
        /// <returns></returns>
        IEnumerable<Employee> Get();

        /// <summary>
        /// Получить сотрудника по id
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns></returns>
        Employee GetById(int id);

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник для добавления</param>
        /// <returns></returns>
        int Add(Employee employee);

        /// <summary>
        /// Редактировать сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник для редактирования</param>
        void Edit(Employee employee);

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id">ИД сотрудника для удаления</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        void SaveChanges();

    }
}
