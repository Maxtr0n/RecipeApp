namespace SharedKernel;
public abstract class BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    protected BaseEntity() { }

    public Guid Id { get; init; }

    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
