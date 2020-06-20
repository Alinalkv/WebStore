using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrustructure.AutoMapperProfiles
{
    public class ViewModelsMapping: Profile
    {
        public ViewModelsMapping()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(view_model => view_model.Brand, opt => opt.MapFrom(product => product.Brand.Name))
                .ReverseMap();

            CreateMap<Employee, EmployeeViewModel>()
                .ReverseMap();
        }
    }
}
