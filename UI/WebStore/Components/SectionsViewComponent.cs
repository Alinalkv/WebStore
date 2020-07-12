using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
       private readonly IProductData _ProductData;
        public SectionsViewComponent (IProductData ProductData)
        {
            _ProductData = ProductData;
        }

        //во вью компоненте дб invoke
        //делаем какую-то логику и возвращаем представление
        public IViewComponentResult Invoke(string SectionId)
        {
            var section_id = int.TryParse(SectionId, out var id) ? id : (int?)null;
            var sections = GetSections(section_id, out var parent_section_id);
            
            return View(new SelectableSectionsViewModel
            { 
             Sections = sections,
             CurrentSectionId = section_id,
             ParentSectionId = parent_section_id
            });
        }

        private IEnumerable<SectionViewModel> GetSections(int? SectionId, out int? ParentSectionId)
        {
            ParentSectionId = null;
            var sections = _ProductData.GetSections().ToArray();
            var parent_sections = sections.Where(c => c.ParentId is null);
            var parent_sections_views = parent_sections.Select(s => new SectionViewModel
            {
                Id = s.Id,
                Order = s.Order,
                Name = s.Name,
            }).ToList();

            foreach (var parent_section in parent_sections_views)
            {
                var child_sections = sections.Where(c => c.ParentId == parent_section.Id);
                foreach (var child in child_sections)
                {
                    if (child.Id == SectionId)
                        ParentSectionId = parent_section.Id;
                    
                    parent_section.ChildSections.Add(new SectionViewModel {
                        Id = child.Id,
                        Order = child.Order,
                        Name = child.Name,
                        ParentSection = parent_section
                    });
                }
                parent_section.ChildSections.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));
            }

            parent_sections_views.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));

            return parent_sections_views;
        }
    }
}
