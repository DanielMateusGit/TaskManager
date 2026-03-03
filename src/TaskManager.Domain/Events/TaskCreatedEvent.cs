namespace TaskManager.Domain.Events;

public class TaskCreatedEvent : IDomainEvent
{
    public Guid Id { get; }
    public string Title { get; set; }
    public DateTime OccurredOn { get; }
    
    public TaskCreatedEvent(Guid taskId, string title)
    {
        this.Id = taskId;
        this.Title = title;
        this.OccurredOn = DateTime.UtcNow;
    }
}