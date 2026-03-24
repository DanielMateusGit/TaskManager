using TaskManager.Domain.Events;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Domain.Entities;
using TaskManager.Domain.ValueObjects;

public class TaskItem
{
    public Guid Id { get; }
    public string Title { get; private set; }
    public Priority Priority { get; private set; }
    public bool IsCompleted => CompletedAt.HasValue;
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents; 
    
    public TaskItem(string title, Priority priority)
    {
        if(string.IsNullOrWhiteSpace(title)) throw new EmptyTaskTitleException();
        this.CreatedAt = DateTime.UtcNow;
        this.Id = Guid.NewGuid();
        this.Title = title;
        this.Priority = priority;
        
        _domainEvents.Add(new TaskCreatedEvent(this.Id, this.Title));
    }

    public void Complete()
    {
        if(IsCompleted) throw new TaskAlreadyCompletedException(this.Id);
        DateTime timeTrack = DateTime.UtcNow;
        this.CompletedAt = timeTrack; 

        _domainEvents.Add(new TaskCompletedEvent(Id, timeTrack));
    }
    public void UpdateTitle(string title)
    {
        if(string.IsNullOrWhiteSpace(title)) throw new EmptyTaskTitleException();
        this.Title = title;
    }
    public void ChangePriority(Priority priority)
    {
        this.Priority = priority;
    }
}
