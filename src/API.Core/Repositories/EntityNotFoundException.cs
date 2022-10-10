using System;

#nullable enable

namespace API.Core.Repositories
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }

        public static EntityNotFoundException FromId<TEntityType>(Guid id)
        {
            var entityType = typeof(TEntityType).Name;
            return new EntityNotFoundException($"{entityType} of id `{id}` does not exist");
        }

        public static EntityNotFoundException FromName<TEntityType>(string name)
        {
            var entityType = typeof(TEntityType).Name;
            return new EntityNotFoundException($"{entityType} of name `{name}` does not exist");
        }
    }
}
