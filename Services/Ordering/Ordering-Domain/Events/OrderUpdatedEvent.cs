namespace Ordering_Domain.Events
{
    public record OrderUpdatedEvent(Order order) : IDomainEvent
    {
    }
}
