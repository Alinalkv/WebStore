using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee> {

            new Employee
            {
                Id = 1,
                FirstName = "Иван",
                SecondName = "Иванович",
                Surname = "Иванов",
                Age = 28
            },
            new Employee
            {
                Id = 2,
                FirstName = "Пётр",
                SecondName = "Петрович",
                Surname = "Петров",
                Age = 29
            },
            new Employee
            {
                Id = 3,
                FirstName = "Алесандр",
                SecondName = "Алесандрович",
                Surname = "Алесандров",
                Age = 28
            },
            };
    }
}
