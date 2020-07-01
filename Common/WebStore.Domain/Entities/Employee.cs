using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
   /// <summary>
   /// Сотрудник магазина
   /// </summary>
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        
        /// <summary>
        /// Отчество
        /// </summary>
        [Required]
        public string SecondName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
       
        /// <summary>
        /// Возраст
        /// </summary>
        [Required]
        public int Age { get; set; }

    }
}
