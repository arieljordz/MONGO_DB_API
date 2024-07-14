using AutoMapper;
using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;

namespace MONGO_DB_API.Mappings.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Employee mappings
            CreateMap<Employee, EmployeeDto>();  // Employee entity to EmployeeDto DTO
            CreateMap<EmployeeDto, Employee>();  // EmployeeDto DTO to Employee entity

            // Position mappings
            CreateMap<Position, PositionDto>();  // Position entity to PositionDto DTO
            CreateMap<PositionDto, Position>();
        }
    }
}
