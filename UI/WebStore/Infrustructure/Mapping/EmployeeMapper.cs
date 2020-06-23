using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Infrustructure.Mapping
{
    public static class EmployeeMapper
    {
        public static EmployeeViewModel ToView(this Employee e) => new EmployeeViewModel { 
         Id = e.Id,
         Age = e.Age,
         FirstName = e.FirstName,
         SecondName = e.SecondName,
         Surname = e.Surname,
        };

        public static Employee FromView(this EmployeeViewModel e) => new Employee { 
         Age = e.Age,
         FirstName = e.FirstName,
         Id = e.Id,
         SecondName = e.SecondName,
         Surname = e.Surname,
        };
    }
}
