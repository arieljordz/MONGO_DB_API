using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MONGO_DB_API.Repositories.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(ObjectId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(ObjectId id, TEntity entity);
        Task DeleteAsync(ObjectId id);
    }
}
