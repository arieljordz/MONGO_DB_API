using AutoMapper;
using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;
using MongoDB.Bson;

namespace MONGO_DB_API.Mappings.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Employee mappings
            CreateMap<Employee, EmployeeDto>(); 
            CreateMap<EmployeeDto, Employee>(); 

            // Department mappings
            CreateMap<Department, DepartmentDto>(); 
            CreateMap<DepartmentDto, Department>();
        }
    }
}
