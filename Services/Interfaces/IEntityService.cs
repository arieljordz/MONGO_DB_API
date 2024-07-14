using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;
using MongoDB.Bson;

namespace MONGO_DB_API.Services.Interfaces
{
    public interface IEntityService<TEntity, TDto> where TEntity : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(ObjectId id);
        Task<TDto> CreateAsync(TEntity entity);
        Task UpdateAsync(ObjectId id, TEntity entity);
        Task DeleteAsync(ObjectId id);
    }   
}
