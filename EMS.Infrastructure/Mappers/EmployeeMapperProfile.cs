using AutoMapper;
using EMS.Infrastructure.Entities;
using EMS.Infrastructure.RequestModels;
using EMS.Infrastructure.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Mappers
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<EmployeeResult, Employee>()
                .ForPath(x => x.Department.Name, opt => opt.MapFrom(o => o.DepartmentName))
                .ReverseMap();
            CreateMap<UpdateEmployeeRequest, Employee>();
            CreateMap<NewEmployeeRequest, Employee>();
        }
    }
}
