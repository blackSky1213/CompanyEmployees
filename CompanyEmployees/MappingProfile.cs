﻿using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployees
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDTO>()
                .ForMember(c => c.FullAddress,
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Employee, EmployeeDTO>().ReverseMap();

            CreateMap<CompanyForCreationDTO, Company>().ReverseMap();
            CreateMap<EmployeeForCreationDTO, Employee>();
            CreateMap<EmployeeForUpdateDTO, Employee>().ReverseMap();
            CreateMap<CompanyForUpdateDTO, Company>();
            CreateMap<UserForRegistrationDTO, User>();
        }
    }
}
