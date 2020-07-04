using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Секция
    /// </summary>
    [Table("ProductSection")]
    public class Section : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Заказ
        /// </summary>
        public int Order { get; set; }
       /// <summary>
       /// id родительской секции
       /// </summary>
        public int? ParentId { get; set; }
        
        /// <summary>
        /// Родительская секция
        /// </summary>
        [ForeignKey(nameof(ParentId))]
        public virtual Section ParentSection { get; set; }

        /// <summary>
        /// Список продуктов секции
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
