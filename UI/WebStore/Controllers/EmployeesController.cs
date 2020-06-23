using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Infrustructure.Interfaces;
using WebStore.Infrustructure.Mapping;

namespace WebStore.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {

        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        public IActionResult Index() => View(_EmployeesData.Get());

        /// <summary>
        /// Детальная информация о сотруднике
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>   
        public IActionResult EmployeeDetails(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }



        #region Редактирование сотрудника
        //генерит вьюшку с формой для редакирования
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int? id, [FromServices] IMapper Mapper)
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
            //return View(employee.ToView()); - без автомэппера
            return View(Mapper.Map<EmployeeViewModel>(employee));
        }

        //после редактирования возвращаемся на Index
        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(EmployeeViewModel model, [FromServices] IMapper Mapper)
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

            // var employee = model.FromView(); - до автомэппера
            var employee = Mapper.Map<Employee>(model);
            
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
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id, [FromServices] IMapper Mapper)
        {
            if (id <= 0)
                return BadRequest();
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();

            //  return View(employee.ToView());
            return View(Mapper.Map<EmployeeViewModel>(employee));
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion


    }
}
