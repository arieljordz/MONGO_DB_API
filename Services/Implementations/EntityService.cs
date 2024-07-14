using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MONGO_DB_API.Repositories.Interfaces;
using MONGO_DB_API.Services.Interfaces;
using AutoMapper;

namespace MONGO_DB_API.Services.Implementations
{
    public class EntityService<TEntity, TDto> : IEntityService<TEntity, TDto> where TEntity : class
    {
        protected readonly IEntityRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public EntityService(IEntityRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(ObjectId id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> CreateAsync(TEntity entity)
        {
            var createdEntity = await _repository.CreateAsync(entity);
            return _mapper.Map<TDto>(createdEntity);
        }

        public async Task UpdateAsync(ObjectId id, TEntity entity)
        {
            await _repository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
