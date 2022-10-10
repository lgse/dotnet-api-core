using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Core.Repositories
{
    public interface IEntityRepository<TEntityType> where TEntityType : UniquelyIdentifiableEntity
    {
        public Task<List<TEntityType>> FindAllAsync();

        public Task<TEntityType[]> FindByConditionAsync(Expression<Func<TEntityType, bool>> expression);

        public Task<TEntityType> FindOneByIdAsync(Guid id, bool isTracked = false);

        public Task<TDto> FindOneByIdAsDtoAsync<TDto>(Guid id);

        public Task<List<TEntityType>> FindMultipleByIdAsync(List<Guid> ids, bool isTracked = false);

        public Task<TEntityType> CreateAsync(TEntityType entity);

        public Task<TEntityType> UpdateAsync(TEntityType entity);

        public Task<TEntityType> DeleteAsync(TEntityType entity);

        public Task<TEntityType> RestoreAsync(TEntityType entity);
    }
}
