using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя является обязательным")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 30 символов")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string SecondName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательной")]
        [MinLength(3, ErrorMessage = "Фамилия должна быть не менее 3 символов")]
        public string Surname { get; set; }

        [Display(Name = "Возраст")]
        [Required]
        [Range(20, 80, ErrorMessage = "Возраст должен быть в пределах то 20 до 80")]
        public int Age { get; set; }
    }
}
