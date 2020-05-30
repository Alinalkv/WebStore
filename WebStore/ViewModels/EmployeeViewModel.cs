using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
}
