using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities
{
    public class ProductFilter
    {
        public int? SectionId { get; set; }
        public int? BrandId { get; set; }
        public int[] Ids { get; set; }

        //номер страницы
        public int Page { get; set; }

        //размер страницы
        public int? PageSize { get; set; }
    }
}
