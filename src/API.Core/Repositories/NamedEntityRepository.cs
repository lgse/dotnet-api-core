using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Core.Extensions;
using AutoMapper;

namespace API.Core.Repositories
{
    public abstract class NamedEntityRepository<TEntityType> : EntityRepository<TEntityType> where TEntityType : NamedEntity
    {
        protected NamedEntityRepository(IMapper mapper, RepositoryContext repositoryContext) : base(mapper, repositoryContext)
        {
        }

        public async Task<TEntityType> FindOneByNameAsync(string name, bool isTracked = false)
        {
            var entity = await RepositoryContext.Set<TEntityType>()
                .AsTracked(isTracked)
                .OrderBy(x => x.CreatedAt)
                .FirstOrDefaultAsync(x => x.Name.Equals(name));

            if (entity == null) {
                throw EntityNotFoundException.FromName<TEntityType>(name);
            }

            return entity;
        }
    }
}
