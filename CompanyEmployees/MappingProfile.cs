using AutoMapper;
using Library.Entities.DataTransferObjects;
using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<Employee, EmployeeForCreationDto>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<Company, CompanyForUpdateDto>().ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<UserForRegistrationDto, User>();



        }
    }
}
