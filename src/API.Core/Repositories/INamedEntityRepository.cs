using System.Threading.Tasks;

namespace API.Core.Repositories
{
    public interface INamedEntityRepository<TEntityType> : IEntityRepository<TEntityType> where TEntityType : NamedEntity
    {
        public Task<TEntityType> FindOneByNameAsync(string name, bool tracked = true);
    }
}
