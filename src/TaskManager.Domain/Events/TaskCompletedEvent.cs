namespace TaskManager.Domain.Events;

public class TaskCompletedEvent : IDomainEvent
{
    public Guid Id { get; }
    public DateTime CompletedAt { get; }
    public DateTime OccurredOn { get; }

    public TaskCompletedEvent(Guid taskId, DateTime completedAt)
    {
        Id = taskId;
        CompletedAt = completedAt;
        OccurredOn = DateTime.UtcNow;
    }
}