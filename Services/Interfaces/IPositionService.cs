using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;
using MongoDB.Bson;

namespace MONGO_DB_API.Services.Interfaces
{
    public interface IPositionService : IEntityService<Position, PositionDto>
    {
    }
}
