using AutoMapper;
using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;
using MONGO_DB_API.Repositories.Interfaces;
using MONGO_DB_API.Services.Interfaces;
using MongoDB.Bson;

namespace MONGO_DB_API.Services.Implementations
{
    public class DepartmentService : EntityService<Department, DepartmentDto>, IDepartmentService
    {
        public DepartmentService(IEntityRepository<Department> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

    }
}
