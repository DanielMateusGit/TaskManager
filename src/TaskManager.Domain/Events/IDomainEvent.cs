namespace TaskManager.Domain.Events;

public interface IDomainEvent
{
    DateTime  OccurredOn { get; }
}