using Microsoft.EntityFrameworkCore;

namespace API.Core.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }
    }
}
