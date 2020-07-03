using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Брэнд
    /// </summary>
    [Table("ProductBrand")]
    public class Brand : NamedEntity, IOrderedEntity
    {
       /// <summary>
       /// Заказ
       /// </summary>
        public int Order { get; set; }
        
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
