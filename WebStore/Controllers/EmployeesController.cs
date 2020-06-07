using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrustructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        public IActionResult Index()
        {
            return View(_EmployeesData.Get());
        }

        public IActionResult EmployeeDetails(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        #region Редактирование сотрудника
        //генерит вьюшку с формой для редакирования
        public IActionResult Edit(int? id)
        {
            //если нулевой id, то открываем пустую форму
            if (id is null)
                return View(new EmployeeViewModel());
            
            //некорректный запос
            if (id < 0)
                return BadRequest();
            var employee = _EmployeesData.GetById((int)id);
            //не нашли сотрудника по id
            if (employee is null)
                return NotFound();
            //нашли, передаём его данные во ViewModel
            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                Surname = employee.Surname,
                Age = employee.Age
            });
        }

        //после редактирования возвращаемся на Index
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            //ошибка конкретного свойства
            if (model.Age < 18 || model.Age > 70)
                ModelState.AddModelError("Age", "Возраст не должен быть менее 18 и более 70 лет");

           //ошибка всей модели
            if (model.FirstName == model.Surname)
                ModelState.AddModelError(string.Empty, "Имя и фамилия не должны совпадать");
            
            //если модель не прошла валидацию
            if (!ModelState.IsValid)
                return View(model);

                var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Surname = model.Surname,
                Age = model.Age
            };

            if (model.Id == 0)
            {
                //создаём нового сотрудника
                _EmployeesData.Add(employee);
            }
            else
            {
                //редакируем сотрудника
                _EmployeesData.Edit(employee);
            }

            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region Удаление сотрудника
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                Surname = employee.Surname,
                Age = employee.Age
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion


    }
}
