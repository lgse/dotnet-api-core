using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Core.Extensions;
using AutoMapper;

namespace API.Core.Repositories
{
    public abstract class EntityRepository<TEntityType> : IEntityRepository<TEntityType> where TEntityType : UniquelyIdentifiableEntity
    {
        protected readonly IMapper Mapper;
        protected readonly RepositoryContext RepositoryContext;

        protected EntityRepository(IMapper mapper, RepositoryContext repositoryContext)
        {
            Mapper = mapper;
            RepositoryContext = repositoryContext;
        }

        public Task<List<TEntityType>> FindAllAsync()
        {
            return RepositoryContext.Set<TEntityType>().ToListAsync();
        }

        public Task<TEntityType[]> FindByConditionAsync(Expression<Func<TEntityType, bool>> expression)
        {
            return RepositoryContext.Set<TEntityType>().Where(expression).ToArrayAsync();
        }

        public virtual async Task<TEntityType> FindOneByIdAsync(Guid id, bool isTracked = false)
        {
            var entity = await RepositoryContext.Set<TEntityType>()
                .AsTracked(isTracked)
                .OrderBy(x => x.CreatedAt)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (entity == null) {
                throw EntityNotFoundException.FromId<TEntityType>(id);
            }

            return entity;
        }

        public async Task<TDto> FindOneByIdAsDtoAsync<TDto>(Guid id)
        {
            var entity = await this.FindOneByIdAsync(id);
            return Mapper.Map<TDto>(entity);
        }

        public Task<List<TEntityType>> FindMultipleByIdAsync(List<Guid> ids, bool isTracked = false)
        {
            return RepositoryContext.Set<TEntityType>()
                .AsTracked(isTracked)
                .Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<TEntityType> CreateAsync(TEntityType entity)
        {
            RepositoryContext.Set<TEntityType>().Add(entity);
            await RepositoryContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntityType> UpdateAsync(TEntityType entity)
        {
            try {
                DetachLocal(entity);
                RepositoryContext.Set<TEntityType>().Update(entity);
                await RepositoryContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                // Do Nothing
            }

            return entity;
        }

        public async Task<TEntityType> DeleteAsync(TEntityType entity)
        {
            DetachLocal(entity);

            if (!entity.IsDeleted()) {
                entity.Delete();
            }

            RepositoryContext.Set<TEntityType>().Update(entity);
            await RepositoryContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntityType> RestoreAsync(TEntityType entity)
        {
            DetachLocal(entity);
            entity.Restore();

            RepositoryContext.Set<TEntityType>().Update(entity);
            await RepositoryContext.SaveChangesAsync();

            return entity;
        }

        protected void DetachLocal(TEntityType entity)
        {
            var local = RepositoryContext.Set<TEntityType>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local is not null) {
                RepositoryContext.Entry(local).State = EntityState.Detached;
            }
        }
    }
}
