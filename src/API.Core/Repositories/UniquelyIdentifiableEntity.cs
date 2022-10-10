using System;

namespace API.Core.Repositories
{
    public abstract class UniquelyIdentifiableEntity
    {
        protected UniquelyIdentifiableEntity()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        public bool IsDeleted()
        {
            return DeletedAt is not null;
        }

        public void Delete()
        {
            DeletedAt = DateTimeOffset.UtcNow;
        }

        public void Restore()
        {
            DeletedAt = null;
        }
    }
}
