namespace Common.Domain;

public class AggregateRoot : BaseEntity
{
    private readonly List<BaseDomainEvent> _events = new List<BaseDomainEvent>();
    public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _events;

    public void AddDomainEvent(BaseDomainEvent eventItem)
    {
        _events.Add(eventItem);
    }

    public void RemoveDomainEvent(BaseDomainEvent eventItem)
    {
        _events?.Remove(eventItem);
    }

}