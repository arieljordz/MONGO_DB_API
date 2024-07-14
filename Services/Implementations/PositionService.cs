using AutoMapper;
using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;
using MONGO_DB_API.Repositories.Interfaces;
using MONGO_DB_API.Services.Interfaces;
using MongoDB.Bson;

namespace MONGO_DB_API.Services.Implementations
{
    public class PositionService : EntityService<Position, PositionDto>, IPositionService
    {
        public PositionService(IEntityRepository<Position> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

    }
}
