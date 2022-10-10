namespace API.Core.Repositories
{
    public abstract class NamedEntity : UniquelyIdentifiableEntity
    {
        public string Name { get; set; }
    }
}
